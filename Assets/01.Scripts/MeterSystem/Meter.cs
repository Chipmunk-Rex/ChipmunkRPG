using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Meter
{
    protected abstract EnumMeterType _meterType { get; }
    private int value;
    public delegate void ValueChanged(int prev, int next);
    public event ValueChanged OnValueChanged;
    public int Value
    {
        get
        {
            return value;
        }
        set
        {
            int value2 = this.value;
            this.value = value;
            this.value = Mathf.Clamp(this.value, 0, MeterData.maxValue);
            if (!value2.Equals(this.value))
            {
                this.OnValueChanged?.Invoke(value2, this.value);
            }
        }
    }
    public MeterData MeterData { get; private set; }
    protected Entity Owner { get; private set; }
    public Meter(Entity owner, MeterData meterData)
    {
        this.MeterData = meterData;
        this.Value = meterData.defaultValue;
        this.Owner = owner;

        OnValueChanged += ValueChangeHandler;
    }
    protected virtual void Initailize()
    {
        Value = MeterData.defaultValue;
    }

    private void ValueChangeHandler(int prev, int next)
    {
        if (next >= MeterData.positiveEffectThreshold)
        {
            OnPositiveEffect();
        }
        if (next <= MeterData.negativeEffectThreshold)
        {
            OnNegativeEffect();
        }
    }

    protected abstract void OnNegativeEffect();

    protected abstract void OnPositiveEffect();
}
