using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBuilder
{
    Vector2Int worldPos;
    private VoronoiNoise voronoiNoise;
    private PerlinNoise perlinNoise;
    private WorldConfigSO worldSO;
    public GroundBuilder Position(Vector2Int worldPos)
    {
        this.worldPos = worldPos;
        return this;
    }
    public GroundBuilder VoronoiNoise(VoronoiNoise voronoiNoise)
    {
        this.voronoiNoise = voronoiNoise;
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
        Ground ground = new Ground(worldPos, groundSO, biome);
        return ground;
    }
    private BiomeSO SelectBiome()
    {
        BiomeSO selectedBiome = null;
        double noiseValue = voronoiNoise.CalculateNoise(worldPos);
        foreach (PieChartData<BiomeSO> biome in worldSO.biomeDatas)
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
        foreach (GroundSO groundData in selectedBiome.groundDatas)
        {
            if (groundData.groundRate >= noiseValue)
            {
                selectedGround = groundData;
                break;
            }
        }
        return selectedGround;
    }
}
