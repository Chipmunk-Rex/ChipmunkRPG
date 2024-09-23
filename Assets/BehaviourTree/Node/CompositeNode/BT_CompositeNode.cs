using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class BT_CompositeNode : BT_Node
{
    [HideInInspector] List<BT_Node> childrenNode = new List<BT_Node>();
    public override void AddChild(BT_Node child)
    {
        childrenNode.Add(child);
    }
    public override void RemoveChild(BT_Node child)
    {
        childrenNode.Remove(child);
    }
    public override List<BT_Node> GetChild()
    {
        return childrenNode;
    }
    public override BT_Node Clone()
    {
        BT_CompositeNode node = Instantiate(this);
        foreach (var child in childrenNode)
        {
            node.AddChild(child.Clone());
        }
        return node;
    }
}
