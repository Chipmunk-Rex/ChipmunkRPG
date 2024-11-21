using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BiomTableSO")]
public class BiomeTable : ScriptableObject
{
    public List<PieChartData<BiomeSO>> biomeDatas = new();
}
