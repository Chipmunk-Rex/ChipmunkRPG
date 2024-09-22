using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[CreateAssetMenu()]
public class BT_BehaviourTree : ScriptableObject
{
    public BT_Node rootNode;
    public BT_EnumNodeState treeState = BT_EnumNodeState.Running;
    public List<BT_Node> nodes = new List<BT_Node>();
    public BT_EnumNodeState UpdateTree()
    {
        if(rootNode.state == BT_EnumNodeState.Running){
            treeState = rootNode.UpdateNode();
        }
        return treeState;
    }
    public BT_Node CreateNode(System.Type type)
    {
        BT_Node node = ScriptableObject.CreateInstance(type) as BT_Node;
        node.name = type.Name;
        node.guid = GUID.Generate().ToString();
        nodes.Add(node);
        
        AssetDatabase.AddObjectToAsset(node, this);
        AssetDatabase.SaveAssets();
        return node;
    }
    public void DeleteNode(BT_Node node)
    {
        nodes.Remove(node);
        AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }
}
