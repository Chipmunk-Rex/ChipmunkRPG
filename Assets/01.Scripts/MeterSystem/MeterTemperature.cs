using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterTemperature : Meter
{
    public MeterTemperature(Entity owner, MeterData meterData) : base(owner, meterData)
    {
    }

    protected override EnumMeterType _meterType => EnumMeterType.Temperature;

    protected override void OnNegativeEffect()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnPositiveEffect()
    {
        throw new System.NotImplementedException();
    }
}
