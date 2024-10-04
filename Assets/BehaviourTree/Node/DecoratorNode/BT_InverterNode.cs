using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_InverterNode : BT_DecoratorNode
{
    public override void OnNodeStart()
    {
    }

    public override void OnNodeStop()
    {
    }

    protected override BT_EnumNodeState OnNodeUpdate()
    {
        switch (childNode.UpdateNode())
        {
            case BT_EnumNodeState.Running:
                return BT_EnumNodeState.Running;
            case BT_EnumNodeState.Success:
                return BT_EnumNodeState.Failure;
            case BT_EnumNodeState.Failure:
                return BT_EnumNodeState.Success;
        }
        return BT_EnumNodeState.Failure;
    }
}
