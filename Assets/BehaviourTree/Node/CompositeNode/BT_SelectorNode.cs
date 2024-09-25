using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_SelectorNode : BT_CompositeNode
{
    int current;
    public override void OnNodeStart()
    {

    }

    public override void OnNodeStop()
    {
    }

    protected override BT_EnumNodeState OnNodeUpdate()
    {
        BT_Node childNode = GetChild()[current];
        switch (childNode.UpdateNode())
        {
            case BT_EnumNodeState.Running:
                return BT_EnumNodeState.Running;
                break;
            case BT_EnumNodeState.Success:
                return BT_EnumNodeState.Success;
                break;
            case BT_EnumNodeState.Failure:
                current++;
                break;
        }
        return current == GetChild().Count ? BT_EnumNodeState.Failure : BT_EnumNodeState.Running;
    }
}
