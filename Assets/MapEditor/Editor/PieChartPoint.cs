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
    public PieChartPoint(PieChartView pieChartView, PieChartData pieChartData)
    {
        this.pieChartView = pieChartView;
        this.chartData = pieChartData;
        this.radius = pieChartView.radius;

        RangeDragAndDropManipulator drangNDropManipulator = new RangeDragAndDropManipulator(this, pieChartView, radius);
        drangNDropManipulator.OnValueChanged += OnValueChanged;
        this.AddManipulator(drangNDropManipulator);

        this.style.width = 50;
        this.style.height = 50;
        this.style.backgroundColor = Color.red;
        this.style.borderTopLeftRadius = 50;
        this.style.borderTopRightRadius = 50;
        this.style.borderBottomLeftRadius = 50;
        this.style.borderBottomRightRadius = 50;
        this.style.position = Position.Absolute;

        this.RegisterCallback<PointerDownEvent>(PointerDownHandler);
    }

    private void OnValueChanged(float angle)
    {
        chartData.percentage = (angle / 360) * 100;
        Debug.Log(chartData.percentage + " " + angle);
        onValueChanged?.Invoke(angle);
    }

    private void PointerDownHandler(PointerDownEvent evt)
    {
        onSelect?.Invoke(this);
    }
}
