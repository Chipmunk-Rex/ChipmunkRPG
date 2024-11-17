using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Meter
{
    protected abstract EnumMeterType _meterType { get; }
    public NotifyValue<int> Value = new();
    protected MeterData MeterData { get; private set; }
    protected Entity Owner { get; private set; }
    public Meter(Entity owner, MeterData meterData)
    {
        this.MeterData = meterData;
        this.Value.Value = meterData.defaultValue;
        this.Owner = owner;

        Value.OnvalueChanged += ValueChangeHandler;
    }
    protected virtual void Initailize()
    {
        Value.Value = MeterData.defaultValue;
    }

    private void ValueChangeHandler(int prev, int next)
    {
        if (next > MeterData.positiveEffectThreshold)
        {
            OnPositiveEffect();
        }
        if (next < MeterData.negativeEffectThreshold)
        {
            OnNegativeEffect();
        }
    }

    protected abstract void OnNegativeEffect();

    protected abstract void OnPositiveEffect();
}
