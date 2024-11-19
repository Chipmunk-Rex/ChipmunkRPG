using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MapData")]
public class WorldConfigSO : ScriptableObject
{
    public string worldName = "New World";
    public List<PieChartData<BiomeSO>> biomeDatas = new();
    public Vector3Int chunkSize = new Vector3Int(5, 5, 5);
    public int biomSize = 3;
    public int biomDetail = 10;
    public int renderSize = 5;
    public float renderDuration = 1f;
    public int depthScale = 3;
    [Tooltip("하루 시간(분)")]
    public int dayDuration = 10;
    public AnimationCurve lightByTime = AnimationCurve.Linear(0, 0, 1, 1);
    public List<(EnumWorldWeather, int)> weathers = new();
}
