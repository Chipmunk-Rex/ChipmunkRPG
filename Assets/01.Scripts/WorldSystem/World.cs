using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class World : MonoBehaviour, IBuildingMap<Building>
{
    public EventMediatorContainer<EnumWorldEvent, WorldEvent> worldEvents = new();
    [SerializeField] WorldConfigSO worldSO;
    [SerializeField] Transform renderTrm;
    [SerializeField] SerializableDictionary<Vector2Int, Ground> grounds = new();
    [field: SerializeField] public List<Entity> entities { get; private set; } = new();
    [field: SerializeField] public Transform entityContainerTrm { get; private set; }
    [SerializeField] int seed = int.MaxValue;
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
    private void Awake()
    {
        this.transform.position = Vector2.zero;

        voronoiNoise = new VoronoiNoise(worldSO.biomSize, seed, worldSO.biomDetail);
        perlinNoise = new PerlinNoise(worldSO.depthScale, seed);

        SetRenderer();

        StartCoroutine(RenderMap());
    }
    public void Initailize(WorldJsonData worldJsonData)
    {
        seed = worldJsonData.seed;
        worldSO = worldJsonData.worldConfigSO;
        Load();
    }

    private void SetRenderer()
    {
        TilemapRenderer groundRenderer = groundTilemap.gameObject.GetComponent<TilemapRenderer>();
        groundRenderer.chunkSize = worldSO.chunkSize;

        TilemapRenderer buildingRenderer = buildingTilemap.gameObject.GetComponent<TilemapRenderer>();
        buildingRenderer.chunkSize = worldSO.chunkSize;
    }

    [SerializeField] BuildingSO buildingSO;
    private void Update()
    {
        // tick = (uint)Mathf.RoundToInt(Time.time * tickRate);

        // if (Input.GetMouseButtonDown(0))
        // {
        //     if (EventSystem.current.IsPointerOverGameObject())
        //     {
        //         Debug.Log("Clicked on the UI");
        //     }
        //     Vector2Int mouseWorldIntPos = Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        //     BaseBuilding building = new BaseBuilding(buildingSO);
        //     ConstructBuilding(building, mouseWorldIntPos);
        // }
    }
    #region SaveLoad
    [ContextMenu("Save")]
    public void Save()
    {
        // WorldJsonSaver.SaveWorld(this);
    }
    public void Save(string path, string jsonData)
    {
        string dirPath = $"{Application.persistentDataPath}/World";
        System.IO.Directory.CreateDirectory(dirPath);
        System.IO.File.WriteAllText($"{dirPath}/{path}", jsonData);
    }
    [ContextMenu("Load")]
    public void Load()
    {
        // World world = WorldJsonSaver.LoadWorld();
        // Load(world);
    }
    public T Load<T>(string path)
    {
        string jsonData = System.IO.File.ReadAllText($"{Application.persistentDataPath}/World/{path}");
        T value = JsonConvert.DeserializeObject<T>(jsonData, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });
        return value;
    }
    public void Load(WorldData worldData)
    {
        grounds.Clear();
        foreach (GroundData groundData in worldData.groundDatas)
        {
            grounds.Add(groundData.worldPos, groundData);
        }
        entities = worldData.entities;

        Destroy(entityContainerTrm.gameObject);
        entityContainerTrm = new GameObject("Entities").transform;
        entityContainerTrm.SetParent(this.transform);
        entityContainerTrm.position = Vector2.zero;
        foreach (var entity in entities)
        {
            Debug.Log(entity);
            Entity spawnedEntity = Instantiate(entity);
            spawnedEntity.transform.SetParent(entityContainerTrm);
        }

        worldSO = worldData.worldConfig;

        foreach (var groundData in grounds)
        {
            groundTilemap.SetTile(Vector3Int.RoundToInt((Vector2)groundData.Key), groundData.Value.groundSO.groundTile);
        }
    }

    #endregion
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

            Vector3 targetPos = Vector2.zero;
            if (renderTrm != null)
                targetPos = renderTrm.position;

            Vector2Int minPos = Vector2Int.RoundToInt(targetPos - new Vector3(worldSO.chunkSize.x * worldSO.renderSize, worldSO.chunkSize.y * worldSO.renderSize));
            Vector2Int maxPos = Vector2Int.RoundToInt(targetPos + new Vector3(worldSO.chunkSize.x * worldSO.renderSize, worldSO.chunkSize.y * worldSO.renderSize));
            GenerateGround(minPos, maxPos);
        }
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
}
