using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Grid))]
public class Chunk : MonoBehaviour
{
    public Ground[,] grounds;
    public Vector2Int ChunkPos { get; private set; }
    private Vector2Int chunkSize;
    private Tilemap groundTilemap;
    private Tilemap buildingTilemap;
    VoronoiNoise voronoiNoise;
    PerlinNoise perlinNoise;
    MapDataSO mapData;
    public void Initialize(Vector2Int chunkPos, Vector2Int chunkSize, VoronoiNoise biomeNoise, PerlinNoise mapNoise, MapDataSO mapData)
    {
        this.ChunkPos = chunkPos;
        this.transform.position = (Vector2)chunkPos * new Vector2(chunkSize.x, chunkSize.y);
        this.chunkSize = chunkSize;

        this.voronoiNoise = biomeNoise;
        this.perlinNoise = mapNoise;

        this.mapData = mapData;

        grounds = new Ground[chunkSize.x, chunkSize.y];

        CreateTilemap();
        GenerateMap();
        
        Tilemap tilemap;
        tilemap.
    }

    private void GenerateMap()
    {
        for (int x = 0; x < chunkSize.x; x++)
            for (int y = 0; y < chunkSize.y; y++)
            {
                Vector3Int worldPos = new Vector3Int(x + (Mathf.RoundToInt(this.transform.position.x)), y + (Mathf.RoundToInt(this.transform.position.y)));

                int voronoiValue = voronoiNoise.CalculateNoise(Vector2Int.RoundToInt((Vector3)worldPos));
                float perlinValue = perlinNoise.CalculateNoise(Vector2Int.RoundToInt((Vector3)worldPos));

                Ground ground = new Ground();

                BiomeSO selectedBiome = SelectBiome(voronoiValue);
                if (selectedBiome == null) continue;
                ground.biome = selectedBiome;


                GroundData groundData = SelectGroundTile(selectedBiome, perlinValue);
                ground.groundType = groundData.groundType;
                Debug.Log(ground.groundType);

                grounds[x, y] = ground;

                Vector3Int cellPos = new Vector3Int(x, y);
                // Vector3Int cellPos = groundTilemap.WorldToCell(worldPos);
                groundTilemap.SetTile(cellPos, groundData.groundTile);
            }
    }

    private BiomeSO SelectBiome(int voronoiNoise)
    {
        BiomeSO selectedBiome = null;
        foreach (BiomeData biome in mapData.biomes)
        {
            BiomeSO biomeSO = biome.biomeSO;
            float biomeRate = biome.biomeRate;

            if (biomeRate > (double)voronoiNoise / int.MaxValue)
            {
                selectedBiome = biomeSO;
                break;
            }
        }

        return selectedBiome;
    }
    private GroundData SelectGroundTile(BiomeSO selectedBiome, float perlinNoise)
    {
        GroundData selectedGround = new();
        foreach (GroundData groundData in selectedBiome.groundDatas.Values)
        {
            if (groundData.biomRate > perlinNoise)
            {
                selectedGround = groundData;
                break;
            }
        }

        return selectedGround;
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

    public Ground GetGround(Vector2Int worldPos)
    {
        Vector2Int groundPos = new Vector2Int(worldPos.x - ChunkPos.x * chunkSize.x, worldPos.y - ChunkPos.y * chunkSize.y);
        Debug.Log(groundPos + " groundPos");
        return grounds[groundPos.x, groundPos.y];
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + new Vector3(chunkSize.x, chunkSize.y) / 2, (Vector2)chunkSize);
    }
#endif
}
