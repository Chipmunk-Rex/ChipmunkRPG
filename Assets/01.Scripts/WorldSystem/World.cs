using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chipmunk.Library.PoolEditor;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class World : MonoSingleton<World>, IBuildingMap<Building>, INDSerializeAble
{
    public EventMediatorContainer<EnumWorldEvent, WorldEvent> worldEvents = new();
    [field: SerializeField] public WorldConfigSO worldSO { get; private set; }
    [SerializeField] Transform renderTrm;
    [field: SerializeField] public SerializableDictionary<Vector2Int, Ground> grounds { get; private set; } = new();
    [field: SerializeField] public List<Entity> entities { get; private set; } = new();
    [field: SerializeField] public Transform entityContainerTrm { get; private set; }
    [field: SerializeField] public int seed { get; private set; } = int.MaxValue;
    [field: SerializeField] public Tilemap groundTilemap { get; private set; }
    [field: SerializeField] public Tilemap buildingTilemap { get; private set; }
    private VoronoiNoise voronoiNoise;
    private PerlinNoise perlinNoise;
    // [SerializeField] uint tickRate = 1;
    // private uint tick = 0;
    private void Reset()
    {
        CreateObject();
    }
    protected override void Awake()
    {
        this.transform.position = Vector2.zero;

        voronoiNoise = new VoronoiNoise(worldSO.biomSize, seed, worldSO.biomDetail);
        perlinNoise = new PerlinNoise(worldSO.depthScale, seed);

        SetRenderer();

        Render();
        // StartCoroutine(RenderMap());
    }

    private void SetRenderer()
    {
        TilemapRenderer groundRenderer = groundTilemap.gameObject.GetComponent<TilemapRenderer>();
        groundRenderer.chunkSize = worldSO.chunkSize;

        TilemapRenderer buildingRenderer = buildingTilemap.gameObject.GetComponent<TilemapRenderer>();
        buildingRenderer.chunkSize = worldSO.chunkSize;
    }
    #region Generate
    public void CreateObject()
    {
        Grid grid = transform.GetComponentInChildren<Grid>();
        if (grid != null)
        {
            Debug.LogWarning($"{gameObject.name} : Grid Is Already Exist, Remove Past Grid");
            GameObject.DestroyImmediate(grid.gameObject);
        }

        GameObject gridObj = new GameObject("Grid");
        gridObj.transform.SetParent(this.transform);
        gridObj.transform.position = new Vector3(-0.5f, -0.5f);
        gridObj.AddComponent<Grid>();

        GameObject groundObj = new GameObject("Ground Tilemap");
        groundObj.transform.SetParent(gridObj.transform);
        groundObj.transform.position = groundObj.transform.parent.transform.position;
        groundTilemap = groundObj.AddComponent<Tilemap>();
        TilemapRenderer groundRenderer = groundObj.AddComponent<TilemapRenderer>();
        groundRenderer.sortingLayerName = "Ground";

        GameObject buildingObj = new GameObject("Building Tilemap");
        buildingObj.transform.SetParent(gridObj.transform);
        buildingObj.transform.position = buildingObj.transform.parent.transform.position;
        buildingTilemap = buildingObj.AddComponent<Tilemap>();
        TilemapRenderer buildingRenderer = buildingObj.AddComponent<TilemapRenderer>();
        buildingRenderer.mode = TilemapRenderer.Mode.Individual;
        buildingRenderer.sortingOrder = 10;

        Transform entityContainerTrm = transform.Find("Entities");
        if (entityContainerTrm == null)
        {
            GameObject entityContainer = new GameObject("Entities");
            entityContainer.transform.SetParent(this.transform);
            entityContainer.transform.position = Vector2.zero;
            entityContainerTrm = entityContainer.transform;
        }
        else
        {
            entityContainerTrm.SetAsLastSibling();
        }
        this.entityContainerTrm = entityContainerTrm;
    }
    // 생각해보면 렌더링을 월드가 하는게 아닌 렌더러가 하는게 맞다.
    // 나중에 고쳐야지;
    public IEnumerator RenderMap()
    {
        StopCoroutine(RenderMap());
        while (true)
        {
            yield return new WaitForSeconds(worldSO.renderDuration);
            Render();
        }
    }

    private void Render()
    {
        Vector3 targetPos = Vector2.zero;
        if (renderTrm != null)
            targetPos = renderTrm.position;

        Vector2Int minPos = Vector2Int.RoundToInt(targetPos - new Vector3(worldSO.chunkSize.x * worldSO.renderSize, worldSO.chunkSize.y * worldSO.renderSize));
        Vector2Int maxPos = Vector2Int.RoundToInt(targetPos + new Vector3(worldSO.chunkSize.x * worldSO.renderSize, worldSO.chunkSize.y * worldSO.renderSize));
        GenerateGround(minPos, maxPos);
    }
    #endregion
    #region Ground
    public void GenerateGround(Vector2Int minPos, Vector2Int maxPos)
    {
        for (int x = minPos.x; x < maxPos.x; x++)
        {
            for (int y = minPos.y; y < maxPos.y; y++)
            {
                Vector2Int worldPos = new Vector2Int(x, y);
                TryCreateGround(worldPos);
            }
        }
    }
    private bool TryCreateGround(Vector2Int worldPos)
    {
        if (grounds.ContainsKey(worldPos)) return false;
        CreateGround(worldPos);
        return true;
    }
    private void CreateGround(Vector2Int worldPos)
    {
        Ground ground = new GroundBuilder()
            .Position(worldPos)
            .VoronoiNoise(voronoiNoise)
            .PerlinNoise(perlinNoise)
            .World(worldSO)
            .Build();

        grounds.Add(worldPos, ground);
        groundTilemap.SetTile(Vector3Int.RoundToInt((Vector2)worldPos), ground.groundSO.groundTile);
    }
    public Ground GetGround(Vector2Int worldPos)
    {
        if (!grounds.ContainsKey(worldPos))
            return null;
        return grounds[worldPos];
    }
    #endregion
    #region building

    public Building GetBuilding(Vector2Int worldPos)
    {
        if (!grounds.ContainsKey(worldPos))
            return null;
        return grounds[worldPos].building;
    }
    public bool CanBuild(Vector2Int worldPos, BuildingSO buildingSO)
    {
        Debug.Log("ming");
        foreach (Vector2Int localPos in buildingSO.tileDatas.Keys)
        {
            Vector2Int tilePos = worldPos + localPos;
            Ground ground = GetGround(tilePos);
            if (ground == null || ground.building != null)
            {
                Debug.Log($"{tilePos} {ground.building} {ground}");
                return false;
            }
        }
        return true;
    }

    public void ConstructBuilding(Building building)
    {
        CreateBuildingEvent @event = new CreateBuildingEvent(this, building, building.pos);
        worldEvents.Execute(EnumWorldEvent.BuildingCreate, @event);
    }
    public void ConstructBuilding(Building building, Vector2Int pos)
    {
        building.pos = pos;
        ConstructBuilding(building);
    }

    public void RemoveBuilding(Vector2Int pos)
    {
        Building building = GetBuilding(pos);

        RemoveBuildingEvent @event = new RemoveBuildingEvent(this, building);
        worldEvents.Execute(EnumWorldEvent.BuildingCreate, @event);

        @event.ExcuteEvent();
    }

    public List<Vector2Int> GetBuildingWolrdPosList(Building building)
    {
        List<Vector2Int> buildingPosList = GetBuildingPosList(building);
        for (int i = 0; i < buildingPosList.Count; i++)
        {
            buildingPosList[i] = buildingPosList[i] + building.pos;
        }
        return buildingPosList;
    }
    public List<Vector2Int> GetBuildingPosList(Building building)
    {
        List<Vector2Int> buildingPosList = new();
        foreach (Vector2Int localPos in building.buildingSO.tileDatas.Keys)
        {
            Vector2Int tilePos = building.pos + localPos;
            buildingPosList.Add(tilePos);
        }
        return buildingPosList;
    }

    public void SetBuilding(Vector2Int pos, Building building)
    {
        throw new NotImplementedException();
    }
    #endregion
    #region Serialize
    [SerializeField] NDSData ndsData = new();
    [ContextMenu("Serialize")]
    public NDSData Serialize()
    {
        NDSData worldNdsData = new NDSData();

        NDSData groundsNDSData = new NDSData();
        foreach (var keyValuePair in grounds)
        {
            NDSData groundNds = keyValuePair.Value.Serialize();
            groundsNDSData.AddData(NDSData.ToString(keyValuePair.Key), groundNds);
        }

        List<NDSData> entitiesNDSData = new List<NDSData>();
        foreach (Entity entity in entities)
        {
            entitiesNDSData.Add(entity.Serialize());
        }

        worldNdsData.AddData("seed", seed);
        worldNdsData.AddData("worldSO", SOAddressSO.Instance.GetIDBySO(worldSO));
        worldNdsData.AddData("grounds", groundsNDSData);
        worldNdsData.AddData("entities", entitiesNDSData);

        this.ndsData = worldNdsData;
        return worldNdsData;
    }
    [ContextMenu("Deserialize")]
    public void Deserialize()
    {
        Deserialize(ndsData);
    }
    public void Deserialize(NDSData data)
    {
        seed = int.Parse(data.GetDataString("seed"));

        worldSO = SOAddressSO.Instance.GetSOByID<WorldConfigSO>(uint.Parse(ndsData.GetDataString("worldSO")));

        grounds.Clear();
        NDSData groundsNDS = data.GetData<NDSData>("grounds");
        foreach (var keyValuePair in data.GetData<NDSData>("grounds").Data)
        {
            Ground ground = new Ground(Vector2Int.zero, null, null);
            ground.Deserialize(groundsNDS.GetData<NDSData>(keyValuePair.Key));
            grounds.Add(NDSData.ToObject<Vector2Int>(keyValuePair.Key), ground);
        }

        entities.Clear();
        List<NDSData> entitiesNDSData = data.GetData<List<NDSData>>("entities");

    }
    #endregion
}
