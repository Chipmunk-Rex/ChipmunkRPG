using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_RootNode : BT_Node
{
    private BT_Node childNode;
    public override void OnNodeStart()
    {
        childNode.OnNodeStart();
    }

    public override void OnNodeStop()
    {
        childNode.OnNodeStop();
    }

    protected override BT_EnumNodeState OnNodeUpdate()
    {
        return childNode.UpdateNode();
    }
    public override void AddChild(BT_Node child)
    {
        childNode = child;
    }
    public override void RemoveChild(BT_Node child)
    {
        childNode = null;
    }

    public override List<BT_Node> GetChild()
    {
        if (childNode == null)
        {
            return new List<BT_Node>();
        }
        return new List<BT_Node> { childNode };
    }
    public override BT_Node Clone()
    {
        BT_RootNode node = Instantiate(this);
        node.childNode = childNode.Clone();
        return node;
    }
}
