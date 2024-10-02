using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class World : MonoBehaviour, IBuildingMap<BaseBuilding>
{
    public EventMediatorContainer<EnumWorldEvent, WorldEvent> worldEvents = new();
    [SerializeField] SerializableDictionary<Vector2Int, Ground> groundDatas = new();
    [field: SerializeField] public List<Entity> entities { get; private set; } = new();
    [field: SerializeField] public Transform entityContainerTrm { get; private set; }
    [SerializeField] int seed = int.MaxValue;
    [field: SerializeField] public Tilemap groundTilemap { get; private set; }
    [field: SerializeField] public Tilemap buildingTilemap { get; private set; }
    private VoronoiNoise voronoiNoise;
    private PerlinNoise perlinNoise;

    [SerializeField] WorldConfigSO mapDataSO;
    private void Reset()
    {
        CreateObject();
    }
    private void Awake()
    {
        this.transform.position = Vector2.zero;

        voronoiNoise = new VoronoiNoise(mapDataSO.biomSize, seed);
        perlinNoise = new PerlinNoise(mapDataSO.depthScale, seed);

        SetRenderer();
        RenderMap();
    }

    private void SetRenderer()
    {
        TilemapRenderer groundRenderer = groundTilemap.gameObject.GetComponent<TilemapRenderer>();
        groundRenderer.chunkSize = mapDataSO.chunkSize;

        TilemapRenderer buildingRenderer = buildingTilemap.gameObject.GetComponent<TilemapRenderer>();
        buildingRenderer.chunkSize = mapDataSO.chunkSize;
    }

    [SerializeField] BuildingSO buildingSO;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");
            }
            Vector2Int mouseWorldIntPos = Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            BaseBuilding building = new BaseBuilding(buildingSO);
            ConstructBuilding(building, mouseWorldIntPos);
        }
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
    public void RenderMap()
    {
        Vector2Int minPos = Vector2Int.RoundToInt(transform.position - new Vector3(mapDataSO.renderSize, mapDataSO.renderSize));
        Vector2Int maxPos = Vector2Int.RoundToInt(transform.position + new Vector3(mapDataSO.renderSize, mapDataSO.renderSize));
        GenerateGround(minPos, maxPos);
    }
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
    #region Ground
    private bool TryCreateGround(Vector2Int worldPos)
    {
        if (groundDatas.ContainsKey(worldPos)) return false;
        CreateGround(worldPos);
        return true;
    }
    private void CreateGround(Vector2Int worldPos)
    {
        Ground ground = new Ground();
        BiomeSO biome = SelectBiome(worldPos);
        if (biome == null) return;
        GroundSO groundSO = SelectGround(biome, worldPos);

        ground.biome = biome;
        ground.groundSO = groundSO;

        groundDatas.Add(worldPos, ground);
        groundTilemap.SetTile(Vector3Int.RoundToInt((Vector2)worldPos), groundSO.groundTile);
    }
    private GroundSO SelectGround(BiomeSO selectedBiome, Vector2Int worldPos)
    {
        GroundSO selectedGround = null;
        float noiseValue = perlinNoise.CalculateNoise(worldPos);
        foreach (GroundSO groundData in selectedBiome.groundDatas.Values)
        {
            if (groundData.groundRate > noiseValue)
            {
                selectedGround = groundData;
                break;
            }
        }

        return selectedGround;
    }
    #endregion
    private BiomeSO SelectBiome(Vector2Int worldPos)
    {
        BiomeSO selectedBiome = null;
        double noiseValue = voronoiNoise.CalculateNoise(worldPos);
        foreach (BiomeData biome in mapDataSO.biomes)
        {
            BiomeSO biomeSO = biome.biomeSO;
            float biomeRate = biome.biomeRate;

            if (biomeRate > noiseValue / int.MaxValue)
            {
                selectedBiome = biomeSO;
                break;
            }
        }

        return selectedBiome;
    }
    #endregion
    #region Ground
    public Ground GetGround(Vector2Int worldPos)
    {
        if (!groundDatas.ContainsKey(worldPos))
            return null;
        return groundDatas[worldPos];
    }
    #endregion
    #region building

    public BaseBuilding GetBuilding(Vector2Int worldPos)
    {
        if (!groundDatas.ContainsKey(worldPos))
            return null;
        return groundDatas[worldPos].building;
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
                return false;
            }
        }
        return true;
    }

    public void ConstructBuilding(BaseBuilding building)
    {
        CreateBuildingEvent @event = new CreateBuildingEvent(this, building, building.pos);
        worldEvents.Execute(EnumWorldEvent.BuildingCreate, @event);
    }
    public void ConstructBuilding(BaseBuilding building, Vector2Int pos)
    {
        building.pos = pos;
        ConstructBuilding(building);
    }

    public void RemoveBuilding(Vector2Int pos)
    {
        BaseBuilding building = GetBuilding(pos);

        RemoveBuildingEvent @event = new RemoveBuildingEvent(this, building);
        worldEvents.Execute(EnumWorldEvent.BuildingCreate, @event);

        @event.ExcuteEvent();
    }

    public List<Vector2Int> GetBuildingWolrdPosList(BaseBuilding building)
    {
        List<Vector2Int> buildingPosList = GetBuildingPosList(building);
        for (int i = 0; i < buildingPosList.Count; i++)
        {
            buildingPosList[i] = buildingPosList[i] + building.pos;
        }
        return buildingPosList;
    }
    public List<Vector2Int> GetBuildingPosList(BaseBuilding building)
    {
        List<Vector2Int> buildingPosList = new();
        foreach (Vector2Int localPos in building.buildingSO.tileDatas.Keys)
        {
            Vector2Int tilePos = building.pos + localPos;
            buildingPosList.Add(tilePos);
        }
        return buildingPosList;
    }

    public void SetBuilding(Vector2Int pos, BaseBuilding building)
    {
        throw new NotImplementedException();
    }
    #endregion
}
