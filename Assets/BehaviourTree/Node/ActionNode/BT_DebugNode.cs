using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_DebugNode : BT_ActionNode
{
    public string message;
    public override void OnNodeStart()
    {
        Debug.Log($"OnNodeStart \n {message}");
    }
    public override void OnNodeStop()
    {
        Debug.Log($"OnNodeStop \n {message}");
    }

    protected override BT_EnumNodeState OnNodeUpdate()
    {
        Debug.Log($"OnNodeUpdate \n {message}");
        return BT_EnumNodeState.Success;
    }
}
