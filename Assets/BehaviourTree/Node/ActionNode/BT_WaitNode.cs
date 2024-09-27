using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_WaitNode : BT_ActionNode
{
    private float duration = 1;
    private float startedTime;
    public override void OnNodeStart()
    {
        startedTime = Time.time;
    }

    public override void OnNodeStop()
    {
    }

    protected override BT_EnumNodeState OnNodeUpdate()
    {
        if (Time.time - startedTime > duration)
        {
            return BT_EnumNodeState.Success;
        }
        return BT_EnumNodeState.Running;
    }
}
