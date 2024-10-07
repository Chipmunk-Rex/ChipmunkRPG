using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PieChartView : VisualElement
{
    float m_Radius = 100.0f;

    public float radius
    {
        get => m_Radius;
        set
        {
            m_Radius = value;
        }
    }

    public float diameter => m_Radius * 2.0f;

    public PieChartData[] pieChartDatas;
    public PieChartPoint[] pieChartPoints;
    public Action<PieChartData> onSelect;
    public Action<PieChartData> onDataChanged;
    public PieChartView(PieChartData[] pieChartDatas, float radius = 100.0f)
    {
        this.pieChartDatas = pieChartDatas;
        this.radius = radius;
        generateVisualContent += DrawCanvas;
        this.style.width = radius * 2;
        this.style.height = radius * 2;
        DrawView(pieChartDatas);
    }

    public void DrawView(PieChartData[] pieChartDatas)
    {
        this.Clear();
        this.pieChartDatas = pieChartDatas;

        CreatePoint(pieChartDatas);
        SetAngle();
        MarkDirtyRepaint();
    }

    public void CreatePoint(PieChartData[] pieChartDatas)
    {
        pieChartPoints = new PieChartPoint[pieChartDatas.Length];

        for (int i = 0; i < pieChartDatas.Length; i++)
        {
            PieChartPoint pieChartPoint = new PieChartPoint(this, pieChartDatas[i]);
            pieChartPoints[i] = pieChartPoint;
            pieChartPoint.onAngleValueChanged += OnPointValueChanged;
            pieChartPoint.onSelect += OnSelectPoint;
            this.Add(pieChartPoint);
        }
    }

    private void OnSelectPoint(PieChartPoint point)
    {
        onSelect?.Invoke(point.chartData);
    }

    private void OnPointValueChanged(float value, PieChartPoint point)
    {
        SetAngle();
        onDataChanged?.Invoke(point.chartData);
    }
    public void SetAngle()
    {
        for (int i = 0; i < pieChartPoints.Length; i++)
        {
            SetMaxAngle(i);
            SetMinAngle(i);
        }
        MarkDirtyRepaint();
    }

    private void SetMinAngle(int index)
    {
        PieChartPoint point = pieChartPoints[index];
        float minAngle = 0;
        if (index > 0)
        {
            minAngle = pieChartPoints[index - 1].angle;
            // Debug.Log("SetMinAngle" + index + " " + minAngle);
        }
        point.SetMinAngle(minAngle);
    }

    private PieChartPoint SetMaxAngle(int index)
    {
        PieChartPoint point = pieChartPoints[index];
        if (index < pieChartPoints.Length - 1)
        {
            point.SetMaxAngle(pieChartPoints[index + 1].angle);
        }
        else
        {
            // Debug.Log("SetMaxAngle" + index);
            float maxAngle = 360.0f;
            point.SetMaxAngle(maxAngle);
        }

        return point;
    }

    void DrawCanvas(MeshGenerationContext ctx)
    {
        Painter2D painter = ctx.painter2D;
        painter.strokeColor = Color.white;
        painter.fillColor = Color.white;

        float anglePct = 0.0f;
        float angleStart = 0.0f;
        var targetDraws = pieChartDatas;

        for (int i = 0; i < targetDraws.Length; i++)
        {
            PieChartData pieChartData = targetDraws[i];
            float angle = 360.0f * (pieChartData.percentage / 100);

            anglePct = angle;


            painter.fillColor = pieChartData.color;
            painter.BeginPath();
            painter.MoveTo(new Vector2(m_Radius, m_Radius));
            painter.Arc(new Vector2(m_Radius, m_Radius), m_Radius, angleStart, anglePct);
            painter.Fill();

            angleStart = angle;
        }
    }
}
