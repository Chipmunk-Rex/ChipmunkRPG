using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PieChartData
{
    [Range(0, 100)]
    public float percentage = 100;
    public Color color = Color.green;
    public PieChartData()
    {
        this.percentage = 1;
        // this.color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
    }
    public PieChartData(float percentage)
    {
        this.percentage = percentage;
        // this.color = new Color(Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
    }
    public PieChartData(float percentage, Color color)
    {
        this.percentage = percentage;
        // this.color = color;
    }
}
[System.Serializable]
public class PieChartData<T> : PieChartData
{
    public T Value;

    public PieChartData(T value, float percentage) : base(percentage)
    {
        this.Value = value;
    }

    public PieChartData(T value, float percentage, Color color) : base(percentage, color)
    {
        this.Value = value;
    }
    public static implicit operator T(PieChartData<T> pieChartData) => pieChartData.Value;
}
