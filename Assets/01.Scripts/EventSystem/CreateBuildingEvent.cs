using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBuildingEvent : BaseEvent
{
    public string debugString = "나 건설시도";
    public override void ExcuteEvent()
    {
        Debug.Log(debugString);
    }
}
