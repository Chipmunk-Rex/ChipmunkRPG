using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class World : MonoBehaviour, IBuildingMap<BaseBuilding>
{
    // [SerializeField] SerializableDictionary<Vector2Int, Chunk> chunkDatas = new();
    EventMediatorContainer<EnumBuildingEvent, BuildingEvent> buildingEventContainer = new();
    [SerializeField] SerializableDictionary<Vector2Int, Ground> groundDatas = new();
    [SerializeField] Vector3Int chunkSize = new Vector3Int(5, 5, 5);
    [SerializeField] int biomSize = 3;
    [SerializeField] int renderSize = 5;
    [SerializeField] int depthScale = 3;
    [SerializeField] int seed = int.MaxValue;
    public Tilemap groundTilemap { get; private set; }
    public Tilemap buildingTilemap { get; private set; }
    private VoronoiNoise voronoiNoise;
    private PerlinNoise perlinNoise;

    [SerializeField] MapDataSO mapData;
    private void Awake()
    {
        this.transform.position = Vector2.zero;

        voronoiNoise = new VoronoiNoise(biomSize, seed);
        perlinNoise = new PerlinNoise(depthScale, seed);

        GenerateMap();
        RenderMap();
        // CreateGround();
        // GenerateChunkMap();
    }
    [SerializeField] BuildingSO buildingSO;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2Int mouseWorldIntPos = Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            BaseBuilding building = new BaseBuilding(buildingSO);
            ConstructBuilding(building, mouseWorldIntPos);
        }
    }
    #region Generate
    public void GenerateMap()
    {
        GameObject gridObj = new GameObject("Grid");
        gridObj.transform.SetParent(this.transform);
        gridObj.transform.position = new Vector3(-0.5f, -0.5f);
        gridObj.AddComponent<Grid>();

        GameObject groundObj = new GameObject("Ground Tilemap");
        groundObj.transform.SetParent(gridObj.transform);
        groundObj.transform.position = groundObj.transform.parent.transform.position;
        groundTilemap = groundObj.AddComponent<Tilemap>();
        TilemapRenderer groundRenderer = groundObj.AddComponent<TilemapRenderer>();
        groundRenderer.chunkSize = chunkSize;
        groundRenderer.sortingLayerName = "Ground";

        GameObject buildingObj = new GameObject("Building Tilemap");
        buildingObj.transform.SetParent(gridObj.transform);
        buildingObj.transform.position = buildingObj.transform.parent.transform.position;
        buildingTilemap = buildingObj.AddComponent<Tilemap>();
        TilemapRenderer buildingRenderer = buildingObj.AddComponent<TilemapRenderer>();
        buildingRenderer.chunkSize = chunkSize;
        buildingRenderer.sortingOrder = 10;

        groundTilemap.SetTile(Vector3Int.zero, tile);
    }
    public void RenderMap()
    {
        Vector2Int minPos = Vector2Int.RoundToInt(transform.position - new Vector3(renderSize, renderSize));
        Vector2Int maxPos = Vector2Int.RoundToInt(transform.position + new Vector3(renderSize, renderSize));
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
        GroundSO selectedGround = new();
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
        foreach (BiomeData biome in mapData.biomes)
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
    [SerializeField] TileBase tile;
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
        CreateBuildingEvent @event = new CreateBuildingEvent(building, this, building.pos);
        buildingEventContainer.Execute(EnumBuildingEvent.CreateBuilding, @event);
    }
    public void ConstructBuilding(BaseBuilding building, Vector2Int pos)
    {
        building.pos = pos;
        ConstructBuilding(building);
    }

    public void RemoveBuilding(Vector2Int pos)
    {
        BaseBuilding building = GetBuilding(pos);

        RemoveBuildingEvent @event = new RemoveBuildingEvent(building, this);
        buildingEventContainer.Execute(EnumBuildingEvent.RemoveBuilding, @event);

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
