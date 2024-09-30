using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoSingleton<MapManager>
{
    Dictionary<Vector2Int, Chunk> chunkDatas = new();
    [SerializeField] Vector2Int chunkSize = new Vector2Int(5, 5);
    [SerializeField] int renderSize = 10;
    [SerializeField] int biomSize = 3;
    [SerializeField] int seed = int.MaxValue;
    VoronoiNoise voronoiNoise;
    protected override void Awake()
    {
        base.Awake();
        this.transform.position = Vector2.zero;

        voronoiNoise = new VoronoiNoise(biomSize, seed);
        GenerateChunkMap();
    }
    private void Update()
    {

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
                Vector2Int position = new Vector2Int(x, y);
                if (chunkDatas.ContainsKey(position))
                    if (chunkDatas[position] != null)
                        continue;
                // Vector2Int chunkPos = new Vector2Int((x / chunkSize.x) * chunkSize.x, (y / chunkSize.y) * chunkSize.y);
                Vector2Int chunkPos = new Vector2Int(x, y);
                if (!chunkDatas.ContainsKey(chunkPos) || chunkDatas[chunkPos] == null)
                {
                    chunkDatas[chunkPos] = MakeChunk(chunkPos);
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
        chunk.Initialize(chunkPos, chunkSize, voronoiNoise);

        return chunk;
    }
    #endregion
    [SerializeField] List<TileBase> tiles;
    public TileBase GetTile(int value)
    {
        int tileValue = value / (int.MaxValue / tiles.Count);
        return tiles[tileValue];
    }
}
