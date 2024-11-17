using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterThirsty : Meter
{
    public MeterThirsty(Entity owner, MeterData meterData) : base(owner, meterData)
    {
    }

    protected override EnumMeterType _meterType => EnumMeterType.Thirsty;

    protected override void OnNegativeEffect()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnPositiveEffect()
    {
        throw new System.NotImplementedException();
    }
}
