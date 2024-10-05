using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "SO/GroundSO")]
public class GroundSO : ScriptableObject
{   
    public EnumGroundType groundType;
    [Range(0.01f, 1)] public float groundRate;
    public TileBase groundTile;
}
