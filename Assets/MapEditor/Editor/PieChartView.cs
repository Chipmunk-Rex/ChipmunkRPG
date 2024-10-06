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

    public PieChartView(PieChartData[] pieChartDatas, float radius = 100.0f)
    {
        this.pieChartDatas = pieChartDatas;
        this.radius = radius;
        generateVisualContent += DrawCanvas;
        this.style.width = radius * 2;
        this.style.height = radius * 2;

        SetPercentages(pieChartDatas);
        MarkDirtyRepaint();
    }
    public void SetPercentages(PieChartData[] pieChartDatas)
    {
        pieChartPoints = new PieChartPoint[pieChartDatas.Length];

        for (int i = 0; i < pieChartDatas.Length; i++)
        {
            PieChartPoint pieChartPoint = new PieChartPoint(this, pieChartDatas[i]);
            pieChartPoints[i] = pieChartPoint;
            pieChartPoint.onValueChanged += v =>
            {
                float maxAngle = 360.0f;
                float sumAngle = 0.0f;
                foreach (PieChartPoint point in pieChartPoints)
                {
                    point.SetMaxAngle(maxAngle - sumAngle);
                    point.SetMinAngle(sumAngle);
                    if (point == pieChartPoint)
                        Debug.Log($"{maxAngle - sumAngle} maxAngle");
                    sumAngle += point.angle;
                }

                MarkDirtyRepaint();
            };
            this.Add(pieChartPoint);
        }
    }

    void DrawCanvas(MeshGenerationContext ctx)
    {
        Painter2D painter = ctx.painter2D;
        painter.strokeColor = Color.white;
        painter.fillColor = Color.white;

        float anglePct = 0.0f;
        float angleSum = 0.0f;
        var targetDraws = pieChartDatas;
        foreach (PieChartData pieChartData in targetDraws)
        {
            float angle = 360.0f * (pieChartData.percentage / 100);
            anglePct = angle + angleSum;

            painter.fillColor = pieChartData.color;
            painter.BeginPath();
            painter.MoveTo(new Vector2(m_Radius, m_Radius));
            painter.Arc(new Vector2(m_Radius, m_Radius), m_Radius, angleSum, anglePct);
            painter.Fill();

            angleSum += angle;
        }
    }
}
