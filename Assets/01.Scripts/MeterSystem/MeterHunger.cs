using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterHunger : Meter
{
    public MeterHunger(Entity owner, MeterData meterData) : base(owner, meterData)
    {
    }

    protected override EnumMeterType _meterType => EnumMeterType.Hunger;

    protected override void OnNegativeEffect()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnPositiveEffect()
    {
        throw new System.NotImplementedException();
    }
}
