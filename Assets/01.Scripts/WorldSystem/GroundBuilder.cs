using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBuilder
{
    Vector2Int worldPos;
    private VoronoiNoise biomeTableNoise;
    private VoronoiNoise biomeNoise;
    private PerlinNoise perlinNoise;
    private WorldConfigSO worldSO;
    public GroundBuilder Position(Vector2Int worldPos)
    {
        this.worldPos = worldPos;
        return this;
    }
    public GroundBuilder VoronoiNoise(VoronoiNoise biomeTableNoise, VoronoiNoise biomeNoise)
    {
        this.biomeTableNoise = biomeTableNoise;
        this.biomeNoise = biomeNoise;
        return this;
    }
    public GroundBuilder PerlinNoise(PerlinNoise perlinNoise)
    {
        this.perlinNoise = perlinNoise;
        return this;
    }
    public GroundBuilder World(WorldConfigSO worldSO)
    {
        this.worldSO = worldSO;
        return this;
    }
    public Ground Build()
    {
        BiomeSO biome = SelectBiome();
        GroundSO groundSO = SelectGround(biome);
        Building building = SelectBuilding(biome);
        Ground ground = new Ground(worldPos, groundSO, biome, building);
        return ground;
    }
    private BiomeTable SelectBiomeTable()
    {
        BiomeTable selectedBiomeTable = null;
        double noiseValue = biomeTableNoise.CalculateNoise(worldPos);
        foreach (PieChartData<BiomeTable> biome in worldSO.biomeTables)
        {
            BiomeTable biomeSO = biome.Value;
            float biomeRate = biome.percentage / 100f;

            if (biomeRate > noiseValue / int.MaxValue)
            {
                selectedBiomeTable = biomeSO;
                break;
            }
        }
        return selectedBiomeTable;
    }
    private BiomeSO SelectBiome()
    {
        BiomeTable selectedBiomeTable = SelectBiomeTable();
        BiomeSO selectedBiome = null;
        double noiseValue = biomeNoise.CalculateNoise(worldPos);
        foreach (PieChartData<BiomeSO> biome in selectedBiomeTable.biomeDatas)
        {
            BiomeSO biomeSO = biome.Value;
            float biomeRate = biome.percentage / 100f;

            if (biomeRate > noiseValue / int.MaxValue)
            {
                selectedBiome = biomeSO;
                break;
            }
        }

        return selectedBiome;
    }
    private GroundSO SelectGround(BiomeSO selectedBiome)
    {
        GroundSO selectedGround = null;
        float noiseValue = perlinNoise.CalculateNoise(worldPos);
        foreach (PieChartData<GroundSO> groundData in selectedBiome.groundDatas)
        {
            if (groundData.percentage >= noiseValue)
            {
                selectedGround = groundData;
                break;
            }
        }
        Debug.Log("Selected Ground: " + selectedGround + "Biome" + selectedBiome);
        return selectedGround;
    }
    private Building SelectBuilding(BiomeSO seletedBiome)
    {
        Building selectedBuilding = null;
        float randomValue = UnityEngine.Random.Range(0f, 100f);

        foreach (PieChartData<BuildingSO> buildingData in seletedBiome.biomeBuildings)
        {
            if (buildingData.percentage >= randomValue)
            {
                selectedBuilding = buildingData.Value.CreateBuilding();
                selectedBuilding.pos = worldPos;
                break;
            }
        }
        return selectedBuilding;
    }
}
