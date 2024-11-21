using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "SO/BiomSO")]
public class BiomeSO : ScriptableObject
{
    public string biomeName;
    public List<PieChartData<BuildingSO>> biomeBuildings = new();
    public List<BiomeEntityData> biomeEntitys = new();
    public List<PieChartData<GroundSO>> groundDatas = new();   
}
