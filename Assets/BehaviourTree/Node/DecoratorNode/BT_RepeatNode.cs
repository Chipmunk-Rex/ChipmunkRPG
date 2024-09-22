using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_RepeatNode : BT_DecoratorNode
{
    public override void OnNodeStart()
    {

    }

    public override void OnNodeStop()
    {
    }

    protected override BT_EnumNodeState OnNodeUpdate()
    {
        childNode.UpdateNode();
        return BT_EnumNodeState.Running;
    }
}
