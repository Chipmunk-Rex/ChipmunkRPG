using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "SO/GroundSO")]
public class GroundSO : ScriptableObject
{   
    public EnumGroundType groundType;
    [JsonIgnore]
    public TileBase groundTile;
    public bool isWater;
}
