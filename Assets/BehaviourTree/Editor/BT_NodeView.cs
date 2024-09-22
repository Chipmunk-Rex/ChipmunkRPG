using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BT_NodeView : Node
{
    BT_Node node;
    public BT_NodeView(BT_Node node){
        this.node = node;
        this.title = node.name;
    }
}
