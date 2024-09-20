using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BT_BehaviourTree : ScriptableObject
{
    public BT_Node rootNode;
    public BT_EnumNodeState treeState = BT_EnumNodeState.Running;

    public BT_EnumNodeState UpdateTree()
    {
        return rootNode.UpdateNode();
    }
}
