using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundData
{
    public readonly Vector2Int worldPos;
    public readonly GroundSO groundSO;
    public readonly BiomeSO biome;
    public readonly Building building;
    public GroundData(Vector2Int worldPos ,GroundSO groundSO, BiomeSO biome, Building building = null)
    {
        this.worldPos = worldPos;
        this.groundSO = groundSO;
        this.biome = biome;
        this.building = building;
    }
    public static implicit operator GroundData(Ground v)
    {
        GroundData groundData = new GroundData(v.WorldPos, v.groundSO, v.biome, v.building);
        return groundData;
    }
    public static implicit operator Ground(GroundData v)
    {
        Ground ground = new Ground(v.worldPos, v.groundSO, v.biome, v.building);
        return ground;
    }
}
