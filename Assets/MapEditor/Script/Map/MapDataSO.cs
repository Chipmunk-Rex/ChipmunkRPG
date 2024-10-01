using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MapData")]
public class MapDataSO : ScriptableObject
{
    public List<BiomeData> biomes = new();
}
