using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PieChartPoint : VisualElement
{
    float radius;

    PieChartView pieChartView;
    public Action<float> onValueChanged;
    public Action<PieChartPoint> onSelect;
    public PieChartData chartData { get; private set; }
    public float angle;
    RangeDragAndDropManipulator drangNDropManipulator;
    public PieChartPoint(PieChartView pieChartView, PieChartData pieChartData)
    {
        this.pieChartView = pieChartView;
        this.chartData = pieChartData;
        this.radius = pieChartView.radius;

        float angle = (pieChartData.percentage / 100) * 360;

        drangNDropManipulator = new RangeDragAndDropManipulator(this, pieChartView, radius);
        drangNDropManipulator.SetPos(angle);
        drangNDropManipulator.OnValueChanged += OnValueChanged;
        this.AddManipulator(drangNDropManipulator);

        SetStyle(pieChartData);
        // SetPos(pieChartData.percentage, pieChartView.radius);

        this.RegisterCallback<PointerDownEvent>(PointerDownHandler);
    }

    private void SetStyle(PieChartData pieChartData)
    {
        this.style.width = 10;
        this.style.height = 10;

        this.style.backgroundColor = pieChartData.color;

        this.style.borderTopColor = Color.black;
        this.style.borderBottomColor = Color.black;
        this.style.borderLeftColor = Color.black;
        this.style.borderRightColor = Color.black;

        this.style.borderTopWidth = 1;
        this.style.borderBottomWidth = 1;
        this.style.borderLeftWidth = 1;
        this.style.borderRightWidth = 1;

        this.style.borderTopLeftRadius = 50;
        this.style.borderTopRightRadius = 50;
        this.style.borderBottomLeftRadius = 50;
        this.style.borderBottomRightRadius = 50;

        this.style.position = Position.Absolute;
    }
    public void SetMaxAngle(float maxAngle)
    {
        if (drangNDropManipulator.Angle > maxAngle)
        {
            drangNDropManipulator.Angle = maxAngle;
        }
        drangNDropManipulator.maxAngle = maxAngle;
    }
    public void SetMinAngle(float minAngle)
    {
        if (drangNDropManipulator.Angle < minAngle)
        {
            drangNDropManipulator.Angle = minAngle;
        }
        drangNDropManipulator.minAngle = minAngle;
    }
    private void OnValueChanged(float angle)
    {
        this.angle = angle;
        chartData.percentage = (angle / 360) * 100;
        Debug.Log(chartData.percentage + " " + angle);
        onValueChanged?.Invoke(angle);
    }

    private void PointerDownHandler(PointerDownEvent evt)
    {
        onSelect?.Invoke(this);
    }
}
