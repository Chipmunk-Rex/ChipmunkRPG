using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
public class Chunk : MonoBehaviour
{
    public EnumBiomType[,] biomTypes;
    public Tilemap tilemap;
    public Vector2Int position;
    private Vector2Int chunkSize;
    private Tilemap groundTilemap;
    private Tilemap buildingTilemap;
    VoronoiNoise noise;
    public void Initialize(Vector2Int position, Vector2Int chunkSize, VoronoiNoise noise)
    {
        this.position = position;
        this.transform.position = (Vector2)position * new Vector2(chunkSize.x, chunkSize.y);
        // this.transform.position = (Vector2)position;
        this.chunkSize = chunkSize;
        this.noise = noise;

        biomTypes = new EnumBiomType[chunkSize.x, chunkSize.y];

        CreateTilemap();
        GenerateMap();
    }

    private void GenerateMap()
    {
        for (int x = 0; x < chunkSize.x; x++)
            for (int y = 0; y < chunkSize.y; y++)
            {
                Vector3Int worldPos = new Vector3Int(x + (Mathf.RoundToInt(this.transform.position.x)), y + (Mathf.RoundToInt(this.transform.position.y)));
                Debug.Log(worldPos);

                int noiseValue = noise.CalculateNoise(Vector2Int.RoundToInt((Vector3)worldPos));
                Debug.Log(noiseValue);
                Vector3Int cellPos = groundTilemap.WorldToCell(worldPos);
                groundTilemap.SetTile(cellPos, MapManager.Instance.GetTile(noiseValue));
            }
    }

    private void CreateTilemap()
    {
        GameObject groundObj = new GameObject();
        groundObj.name = "Ground Tilemap";
        groundObj.transform.SetParent(this.transform);
        groundObj.transform.position = this.transform.position;
        groundTilemap = groundObj.AddComponent<Tilemap>();
        groundObj.AddComponent<TilemapRenderer>();

        GameObject buildingObj = new GameObject();
        buildingObj.name = "Building Tilemap";
        buildingObj.transform.SetParent(this.transform);
        buildingObj.transform.position = this.transform.position;
        buildingTilemap = buildingObj.AddComponent<Tilemap>();
        buildingObj.AddComponent<TilemapRenderer>();
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(chunkSize.x, chunkSize.y) / 2, (Vector2)chunkSize);
    }
#endif
}
