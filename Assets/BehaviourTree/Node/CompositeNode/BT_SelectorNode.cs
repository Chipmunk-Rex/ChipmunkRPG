using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_SelectorNode : BT_CompositeNode
{
    int current;
    public override void OnNodeStart()
    {
        current = 0;
    }

    public override void OnNodeStop()
    {
    }

    protected override BT_EnumNodeState OnNodeUpdate()
    {
        while (current < GetChild().Count) 
        {
            BT_Node childNode = GetChild()[current];
            switch (childNode.UpdateNode())
            {
                case BT_EnumNodeState.Running:
                    return BT_EnumNodeState.Running;
                case BT_EnumNodeState.Success:
                    return BT_EnumNodeState.Success;
                case BT_EnumNodeState.Failure:
                    current++; 
                    break;
            }
        }
        return BT_EnumNodeState.Failure;
    }
}
