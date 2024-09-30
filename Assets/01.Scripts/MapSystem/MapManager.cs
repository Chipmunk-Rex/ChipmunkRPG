using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoSingleton<MapManager>
{
    [SerializeField] SerializableDictionary<Vector2Int, Chunk> chunkDatas = new();
    [SerializeField] Vector2Int chunkSize = new Vector2Int(5, 5);
    [SerializeField] int renderSize = 10;
    [SerializeField] int biomSize = 3;
    [SerializeField] int perlinNoiseScale = 3;
    [SerializeField] int seed = int.MaxValue;
    VoronoiNoise voronoiNoise;
    PerlinNoise perlinNoise;

    [SerializeField] MapDataSO mapData;
    protected override void Awake()
    {
        base.Awake();
        this.transform.position = Vector2.zero;

        voronoiNoise = new VoronoiNoise(biomSize, seed);
        perlinNoise = new PerlinNoise(perlinNoiseScale, seed);

        GenerateChunkMap();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2Int mousePos = Vector2Int.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            Debug.Log(mousePos);
            Ground ground = chunkDatas[mousePos].GetGround(mousePos);
            Debug.Log(ground.groundType.ToString());
        }
    }
    #region Map
    public void GenerateMap()
    {

    }
    #endregion

    #region Chunk
    public void GenerateChunkMap()
    {
        Vector2 camPos = Camera.main.transform.position;
        Vector2Int camPosInt = new Vector2Int(Mathf.RoundToInt(camPos.x), Mathf.RoundToInt(camPos.y));

        GenerateChunkMap(camPosInt - new Vector2Int(renderSize, renderSize), camPosInt + new Vector2Int(renderSize, renderSize));
    }
    public void GenerateChunkMap(Vector2Int minPos, Vector2Int maxPos)
    {
        Debug.Log($"{minPos} {maxPos}");
        for (int x = minPos.x; x < maxPos.x; x++)
        {
            for (int y = minPos.y; y < maxPos.y; y++)
            {
                Vector2Int chunkPos = new Vector2Int(x, y);
                Vector2Int worldPos = new Vector2Int(x, y) * new Vector2Int(chunkSize.x, chunkSize.y);
                if (chunkDatas.ContainsKey(worldPos))
                    if (chunkDatas[worldPos] != null)
                        continue;

                if (!chunkDatas.ContainsKey(worldPos) || chunkDatas[worldPos] == null)
                {
                    chunkDatas[worldPos] = MakeChunk(chunkPos);
                }
            }
        }
    }
    private Chunk MakeChunk(Vector2Int chunkPos)
    {
        GameObject gameObject = new GameObject();
        gameObject.transform.SetParent(this.transform);
        gameObject.name = $"Chunk {chunkPos.x}, {chunkPos.y}";

        Chunk chunk = gameObject.AddComponent<Chunk>();
        chunk.Initialize(chunkPos, chunkSize, voronoiNoise, perlinNoise, mapData);

        Vector2Int worldPos = chunkPos * new Vector2Int(chunkSize.x, chunkSize.y);
        for (int x = 0; x < chunkSize.x; x++)
            for (int y = 0; y < chunkSize.y; y++)
            {
                Vector2Int worldGroundPos = worldPos + new Vector2Int(x, y);
                chunkDatas[worldGroundPos] = chunk;
            }


        return chunk;
    }
    #endregion
}
