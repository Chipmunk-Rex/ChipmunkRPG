using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PieChartView : VisualElement
{
    float m_Radius = 100.0f;
    float m_Value = 40.0f;

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

    public PieChartView(PieChartData[] pieChartDatas)
    {
        generateVisualContent += DrawCanvas;
        this.style.width = radius * 2;
        this.style.height = radius * 2;

        SetPercentages(pieChartDatas);
        MarkDirtyRepaint();
    }
    public void SetPercentages(PieChartData[] pieChartDatas)
    {
        this.pieChartDatas = pieChartDatas;
        for (int i = 0; i < pieChartDatas.Length; i++)
        {
            PieChartPoint pieChartPoint = new PieChartPoint(this, pieChartDatas[i]);
            pieChartPoint.onValueChanged += v =>
            {
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

        var colors = new Color32[] {
            new Color32(182,235,122,255),
            new Color32(251,120,19,255)
        };
        float angle = 0.0f;
        float anglePct = 0.0f;
        foreach (PieChartData pieChartData in pieChartDatas)
        {
            anglePct += 360.0f * (pieChartData.percentage / 100);

            painter.fillColor = pieChartData.color;
            painter.BeginPath();
            painter.MoveTo(new Vector2(m_Radius, m_Radius));
            painter.Arc(new Vector2(m_Radius, m_Radius), m_Radius, angle, anglePct);
            painter.Fill();

            angle = anglePct;
        }
    }
}
