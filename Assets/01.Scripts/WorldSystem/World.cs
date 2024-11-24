using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chipmunk.Library.PoolEditor;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
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
    [field: SerializeField] public Tilemap lowerBuildingTilemap { get; private set; }
    [field: SerializeField] public Tilemap buildingTilemap { get; private set; }
    [field: SerializeField] public Tilemap groundTilemap { get; private set; }
    [field: SerializeField] public Tilemap waterTilemap { get; private set; }
    [field: SerializeField] public EntityCompo playerCompo { get; private set; }
    [field: SerializeField] public Player player => playerCompo.Entity as Player;
    private VoronoiNoise biomeTableNoise;
    private VoronoiNoise biomeNoise;
    private PerlinNoise perlinNoise;
    [field: SerializeField] public int Day { get; private set; }

    [field: SerializeField]
    public SerializeableNotifyValue<int> Time { get; private set; } = new SerializeableNotifyValue<int>();

    public UnityEvent OnWorldInitComplete;

    [SerializeField] Light2D dayLight;

    // [SerializeField] uint tickRate = 1;
    // private uint tick = 0;
    private void Reset()
    {
        CreateObject();
    }

    protected override void Awake()
    {
        this.transform.position = Vector2.zero;

        biomeTableNoise = new VoronoiNoise(worldSO.biomTableSize, seed, worldSO.biomDetail);
        biomeNoise = new VoronoiNoise(worldSO.biomSize, seed, worldSO.biomDetail);
        perlinNoise = new PerlinNoise(worldSO.depthScale, seed);

        SetRenderer();

        OnWorldInitComplete?.Invoke();
        // StartCoroutine(RenderMap());
    }

    private void FixedUpdate()
    {
        float time = ((UnityEngine.Time.time * 40) / 60);
        Time.Value = Mathf.RoundToInt(time % worldSO.dayDuration); // 1시간 = 1분

        Day = Mathf.RoundToInt(time) / worldSO.dayDuration; // 1일 = 시간 나누기 하루 길이의 나머지

        float calculateTime = time % worldSO.dayDuration / worldSO.dayDuration; // 0 ~ 1로 정규화된 시간
        float lightIntensity = worldSO.lightByTime.Evaluate(calculateTime);
        Color lightColor = worldSO.lightColor.Evaluate(calculateTime);
        // Debug.Log(lightIntensity);
        dayLight.color = lightColor;
        dayLight.intensity = lightIntensity;
    }

    private void SetRenderer()
    {
        Vector3Int chunkSize = new Vector3Int(worldSO.chunkSize.x * worldSO.renderSize, worldSO.chunkSize.y * worldSO.renderSize, 1);

        TilemapRenderer groundRenderer = groundTilemap.gameObject.GetComponent<TilemapRenderer>();
        groundRenderer.chunkSize = chunkSize;
        TilemapRenderer waterRenderer = waterTilemap.gameObject.GetComponent<TilemapRenderer>();
        waterRenderer.chunkSize = chunkSize;

        TilemapRenderer buildingRenderer = buildingTilemap.gameObject.GetComponent<TilemapRenderer>();
        buildingRenderer.chunkSize = chunkSize;

        TilemapRenderer lowBuildingRenderer = lowerBuildingTilemap.gameObject.GetComponent<TilemapRenderer>();
        lowBuildingRenderer.chunkSize = chunkSize;

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

        Vector2Int minPos = Vector2Int.RoundToInt(targetPos - new Vector3(worldSO.chunkSize.x * worldSO.renderSize,
            worldSO.chunkSize.y * worldSO.renderSize));
        Vector2Int maxPos = Vector2Int.RoundToInt(targetPos + new Vector3(worldSO.chunkSize.x * worldSO.renderSize,
            worldSO.chunkSize.y * worldSO.renderSize));
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
            .VoronoiNoise(biomeTableNoise, biomeNoise)
            .PerlinNoise(perlinNoise)
            .World(worldSO)
            .Build();

        grounds.Add(worldPos, ground);
        Debug.Log(ground);
        Debug.Log(ground.groundSO.name);
        Debug.Log(ground.groundSO.isWater);
        if (!ground.groundSO.isWater)
            groundTilemap.SetTile(Vector3Int.RoundToInt((Vector2)worldPos), ground.groundSO.groundTile);
        else
            waterTilemap.SetTile(Vector3Int.RoundToInt((Vector2)worldPos), ground.groundSO.groundTile);

        // 나도 이것이 해괴하고 난잡한 코드인것을 안다. 하지만 지금은 시간이 매우 부족하고 이미 만든 코드를 수정하는것보다 오류를 피하면서 새로운 코드를 작성하는게 더 나은 선택이라고 생각한다.
        Building building = ground.building;
        ground.building = null;
        if (building != null)
            CreateBuilding(building);
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
        if(CanBuild(worldPos) == false)
            return false;
        foreach (Vector2Int localPos in buildingSO.tileDatas.Keys)
        {
            Vector2Int tilePos = worldPos + localPos;
            if (!CanBuild(tilePos))
                return false;
        }

        return true;
    }

    public bool CanBuild(Vector2Int worldPos)
    {
        Ground ground = GetGround(worldPos);
        if (ground == null || ground.building != null)
        {
            Debug.Log("CanBuild : ground is null or building is already exist");
            return false;
        }

        return true;
    }

    public void CreateBuilding(Building building)
    {
        CreateBuildingEvent @event = new CreateBuildingEvent(this, building, building.pos);
        worldEvents.Execute(EnumWorldEvent.BuildingCreate, @event);
    }

    public void CreateBuilding(Building building, Vector2Int pos)
    {
        building.pos = pos;
        CreateBuilding(building);
    }

    public void CreateBuilding(Vector2Int pos, BuildingSO buildingSO)
    {
        Building building = buildingSO.CreateBuilding();
        CreateBuilding(pos, building);
    }

    public void CreateBuilding(Vector2Int pos, Building building)
    {
        WorldEvent @event = new CreateBuildingEvent(this, building, Vector2Int.RoundToInt(pos));
        this.worldEvents.Execute(EnumWorldEvent.BuildingCreate, @event);
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
            if (entity is Player) continue;
            NDSData entityNDSData = entity.Serialize();
            entitiesNDSData.Add(entityNDSData);
        }

        worldNdsData.AddData("Player", player.Serialize());
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
        groundTilemap.ClearAllTiles();
        waterTilemap.ClearAllTiles();
        NDSData groundsNDS = data.GetData<NDSData>("grounds");
        foreach (var keyValuePair in data.GetData<NDSData>("grounds").Data)
        {
            Ground ground = new Ground(Vector2Int.zero, null, null);
            ground.Deserialize(groundsNDS.GetData<NDSData>(keyValuePair.Key));
            Vector2Int pos = NDSData.ToObject<Vector2Int>(keyValuePair.Key);
            CreateGround(pos);
            grounds.Add(pos, ground);
        }

        foreach (Entity entity in entities)
        {
            if (entity is Player) continue;
            PoolManager.Instance.Push(entity.entityCompo as IPoolAble);
        }

        entities.Clear();
        List<NDSData> entitiesNDSData = data.GetData<List<NDSData>>("entities");
        foreach (NDSData entityNDSData in entitiesNDSData)
        {
            EntitySO entitySO =
                SOAddressSO.Instance.GetSOByID<EntitySO>(uint.Parse(entityNDSData.GetDataString("EntitySO")));
            Entity entity = entitySO.CreateEntity();
            entity.Deserialize(entityNDSData);
            if (!entity.hasOwner)
            {
                entity.SpawnEntity(this);
                entities.Add(entity);
            }
        }

        player.Deserialize(data.GetData<NDSData>("Player"));
    }

    internal Vector2Int GetTilePos(Vector3 vector3)
    {
        return Vector2Int.RoundToInt(vector3);
    }

    #endregion
}