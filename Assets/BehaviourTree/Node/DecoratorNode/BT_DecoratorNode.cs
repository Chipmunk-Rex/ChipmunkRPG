using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class BT_DecoratorNode : BT_Node
{
    [HideInInspector] public BT_Node childNode;
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
        BT_DecoratorNode node = Instantiate(this);
        node.childNode = childNode.Clone();
        return node;
    }
}

