using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoSingleton<MapManager>
{
    // [SerializeField] SerializableDictionary<Vector2Int, Chunk> chunkDatas = new();
    [SerializeField] SerializableDictionary<Vector2Int, Ground> groundDatas = new();
    [SerializeField] Vector3Int chunkSize = new Vector3Int(5, 5, 5);
    [SerializeField] int biomSize = 3;
    [SerializeField] int renderSize = 5;
    [SerializeField] int depthScale = 3;
    [SerializeField] int seed = int.MaxValue;
    private Tilemap groundTilemap;
    private Tilemap buildingTilemap;
    private VoronoiNoise voronoiNoise;
    private PerlinNoise perlinNoise;

    [SerializeField] MapDataSO mapData;
    protected override void Awake()
    {
        base.Awake();
        this.transform.position = Vector2.zero;

        voronoiNoise = new VoronoiNoise(biomSize, seed);
        perlinNoise = new PerlinNoise(depthScale, seed);

        GenerateMap();
        RenderMap();
        // CreateGround();
        // GenerateChunkMap();
    }
    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Vector2Int mousePos = Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        //     Debug.Log(mousePos);
        //     Ground ground = chunkDatas[mousePos].GetGround(mousePos);
        //     Debug.Log(ground.groundType.ToString());
        // }
    }
    #region Map
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


        GameObject buildingObj = new GameObject("Building Tilemap");
        buildingObj.transform.SetParent(gridObj.transform);
        buildingObj.transform.position = buildingObj.transform.parent.transform.position;
        buildingTilemap = buildingObj.AddComponent<Tilemap>();
        TilemapRenderer buildingRenderer = buildingObj.AddComponent<TilemapRenderer>();
        buildingRenderer.chunkSize = chunkSize;

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
        if(biome == null) return;
        GroundSO groundSO = SelectGround(biome, worldPos);

        ground.biome = biome;
        ground.groundSO = groundSO;

        groundDatas.Add(worldPos, ground);
        groundTilemap.SetTile(Vector3Int.RoundToInt((Vector2) worldPos), groundSO.groundTile);
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

    #region Chunk
    // public void GenerateChunkMap()
    // {
    //     Vector2 camPos = Camera.main.transform.position;
    //     Vector2Int camPosInt = new Vector2Int(Mathf.RoundToInt(camPos.x), Mathf.RoundToInt(camPos.y));

    //     GenerateChunkMap(camPosInt - new Vector2Int(renderSize, renderSize), camPosInt + new Vector2Int(renderSize, renderSize));
    // }
    // public void GenerateChunkMap(Vector2Int minPos, Vector2Int maxPos)
    // {
    //     for (int x = minPos.x; x < maxPos.x; x++)
    //     {
    //         for (int y = minPos.y; y < maxPos.y; y++)
    //         {
    //             Vector2Int chunkPos = new Vector2Int(x, y);
    //             Vector2Int worldPos = new Vector2Int(x, y) * new Vector2Int(chunkSize.x, chunkSize.y);
    //             if (chunkDatas.ContainsKey(worldPos))
    //                 if (chunkDatas[worldPos] != null)
    //                     continue;

    //             if (!chunkDatas.ContainsKey(worldPos) || chunkDatas[worldPos] == null)
    //             {
    //                 chunkDatas[worldPos] = MakeChunk(chunkPos);
    //             }
    //         }
    //     }
    // }
    // private Chunk MakeChunk(Vector2Int chunkPos)
    // {
    //     GameObject gameObject = new GameObject();
    //     gameObject.transform.SetParent(this.transform);
    //     gameObject.name = $"Chunk {chunkPos.x}, {chunkPos.y}";

    //     Chunk chunk = gameObject.AddComponent<Chunk>();
    //     chunk.Initialize(chunkPos, chunkSize, voronoiNoise, perlinNoise, mapData);

    //     Vector2Int worldPos = chunkPos * new Vector2Int(chunkSize.x, chunkSize.y);
    //     for (int x = 0; x < chunkSize.x; x++)
    //         for (int y = 0; y < chunkSize.y; y++)
    //         {
    //             Vector2Int worldGroundPos = worldPos + new Vector2Int(x, y);
    //             chunkDatas[worldGroundPos] = chunk;
    //         }


    //     return chunk;
    // }
    #endregion
}
