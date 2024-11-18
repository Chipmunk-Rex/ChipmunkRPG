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
    private int defaultWidth = 235;
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
    public void Initailize(Dictionary<EnumMeterType, Meter> meters)
    {
        foreach (KeyValuePair<EnumMeterType, Meter> meterPair in meters)
        {
            SetMeterBar(meterPair.Key, meterPair.Value);
            meterPair.Value.OnValueChanged += (prev, next) => SetMeterBar(meterPair.Key, meterPair.Value);
        }
    }

    private void SetMeterBar(EnumMeterType meterType, Meter meter)
    {
        if (meter.MeterData.maxValue == 0) return;
        Meters[meterType].Q<VisualElement>("Value").style.width = defaultWidth * meter.Value / meter.MeterData.maxValue;
    }
}
