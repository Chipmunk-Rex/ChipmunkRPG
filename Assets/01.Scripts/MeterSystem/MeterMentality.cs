using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterMentality : Meter
{
    public MeterMentality(Entity owner, MeterData meterData) : base(owner, meterData)
    {
    }

    protected override EnumMeterType _meterType => EnumMeterType.Mentality;

    protected override void OnNegativeEffect()
    {
        throw new System.NotImplementedException();
    }

    protected override void OnPositiveEffect()
    {
        throw new System.NotImplementedException();
    }
}
