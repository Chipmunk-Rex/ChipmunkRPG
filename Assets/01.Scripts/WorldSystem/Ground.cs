using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Ground
{
    Vector2Int worldPos;
    public GroundSO groundSO;
    public BiomeSO biome;
    public BaseBuilding building;
    public Ground(Vector2Int worldPos ,GroundSO groundSO, BiomeSO biome, BaseBuilding building = null)
    {
        this.worldPos = worldPos;
        this.groundSO = groundSO;
        this.biome = biome;
        this.building = building;
    }
}
