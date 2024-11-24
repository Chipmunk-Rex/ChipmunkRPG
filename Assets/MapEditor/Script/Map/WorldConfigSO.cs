using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/MapData")]
public class WorldConfigSO : ScriptableObject
{
    public string worldName = "New World";
    public List<PieChartData<BiomeTable>> biomeTables = new();
    public Vector2Int chunkSize = new(5, 5);
    public int biomSize = 2;
    public int biomTableSize = 6;
    public int biomDetail = 10;
    public int renderSize = 5;
    public float renderDuration = 1f;
    public int depthScale = 3;
    [Tooltip("하루 시간(분)")]
    public int dayDuration = 10;
    [Tooltip("주기 일수(일)")]
    public int cycleDuration = 7;
    public AnimationCurve lightByTime = AnimationCurve.Linear(0, 0, 1, 1);
    public Gradient lightColor = new Gradient();
    public List<(EnumWorldWeather, int)> weathers = new();
}
