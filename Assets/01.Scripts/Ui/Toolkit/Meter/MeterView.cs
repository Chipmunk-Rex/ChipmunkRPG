using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;
using UnityEngine.UIElements;

public class MeterView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<MeterView, VisualElement.UxmlTraits>
    {
    }
    public Dictionary<EnumMeterType, VisualElement> Meters { get; private set; } = new();
    public MeterView()
    {
        foreach (EnumMeterType meterType in System.Enum.GetValues(typeof(EnumMeterType)))
        {
            VisualElement meterView = new VisualElement();
            meterView.name = meterType.ToString();
            meterView.AddToClassList("meter");

            VisualElement meterValue = new VisualElement();
            meterValue.name = "Value";
            meterView.Add(meterValue);

            VisualElement meterBackground = new VisualElement();
            meterBackground.name = "Background";
            meterView.Add(meterBackground);

            VisualElement meterVisual = new VisualElement();
            meterVisual.name = "Visual";
            meterView.Add(meterVisual);

            Meters.Add(meterType, meterView);
            Add(meterView);
        }
    }
}
