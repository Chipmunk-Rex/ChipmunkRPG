using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MapData")]
public class WorldConfigSO : ScriptableObject
{
    public List<PieChartData<WorldBiomeData>> biomes = new();
    public Vector3Int chunkSize = new Vector3Int(5, 5, 5);
    public int biomSize = 3;
    public int biomDetail = 10;
    public int renderSize = 5;
    public float renderDuration = 1f;
    public int depthScale = 3;
}
