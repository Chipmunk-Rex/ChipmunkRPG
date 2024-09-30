using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
public struct GroundData
{   
    public EnumGroundType groundType;
    [Range(0.01f, 1)] public float biomRate;
    public TileBase groundTile;
}
