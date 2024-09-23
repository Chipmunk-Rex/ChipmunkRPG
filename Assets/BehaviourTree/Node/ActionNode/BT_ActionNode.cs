using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class BT_ActionNode : BT_Node
{
    public Node Child;
    public sealed override void AddChild(BT_Node child) { }
    public sealed override void RemoveChild(BT_Node child) { }
    public override List<BT_Node> GetChild()
    {
        return new List<BT_Node>();
    }
    public override BT_Node Clone()
    {
        BT_ActionNode node = Instantiate(this);
        return node;
    }
}
