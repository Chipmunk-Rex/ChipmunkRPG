using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MeterData
{
    public int maxValue;
    public int positiveEffectThreshold;
    public int negativeEffectThreshold;
    public int defaultValue;
    public MeterData(int maxValue = 100, int positiveEffectThreshold = 70, int negativeEffectThreshold = 30, int defaultValue = 50)
    {
        this.maxValue = maxValue;
        this.positiveEffectThreshold = positiveEffectThreshold;
        this.negativeEffectThreshold = negativeEffectThreshold;
        this.defaultValue = defaultValue;
    }
}
