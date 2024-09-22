using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class BT_CompositeNode : BT_Node
{
    List<BT_Node> childrenNode = new List<BT_Node>();
}
