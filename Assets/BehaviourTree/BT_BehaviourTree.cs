using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[CreateAssetMenu()]
public class BT_BehaviourTree : ScriptableObject
{
    public BT_RootNode rootNode;
    public BT_EnumNodeState treeState = BT_EnumNodeState.Running;
    public List<BT_Node> nodes = new List<BT_Node>();
    public BT_EnumNodeState UpdateTree()
    {
        if (rootNode.state == BT_EnumNodeState.Running)
        {
            treeState = rootNode.UpdateNode();
        }
        return treeState;
    }
    public BT_Node CreateNode(System.Type type)
    {
        BT_Node node = ScriptableObject.CreateInstance(type) as BT_Node;
        node.name = type.Name;
        node.guid = GUID.Generate().ToString();

        Undo.RecordObject(this, "BehaviourTree Create Node");
        nodes.Add(node);
        EditorUtility.SetDirty(this);

        AssetDatabase.AddObjectToAsset(node, this);
        Undo.RegisterCreatedObjectUndo(node, "BehaviourTree Create Node");
        AssetDatabase.SaveAssets();
        return node;
    }
    public void DeleteNode(BT_Node node)
    {
        Undo.RecordObject(this, "BehaviourTree Delete Node");
        nodes.Remove(node);
        // AssetDatabase.RemoveObjectFromAsset(node);
        Undo.DestroyObjectImmediate(node);
        AssetDatabase.SaveAssets();
    }
    public void AddChildNode(BT_Node parent, BT_Node child)
    {
        Undo.RecordObject(this, "BehaviourTree Add Child Node");
        Undo.RecordObject(parent, "BehaviourTree Add Child Node");
        parent.AddChild(child);
        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(parent);
    }
    public void RemoveChildNode(BT_Node parent, BT_Node child)
    {
        Undo.RecordObject(parent, "BehaviourTree Remove Child Node");
        parent.RemoveChild(child);
        EditorUtility.SetDirty(parent);
    }
    public List<BT_Node> GetChildren(BT_Node parentNode)
    {
        return parentNode.GetChild();
    }
    public BT_BehaviourTree Clone()
    {
        BT_BehaviourTree tree = Instantiate(this);
        tree.rootNode = rootNode.Clone() as BT_RootNode;
        tree.nodes = new List<BT_Node>();
        foreach (var node in nodes)
        {
            tree.nodes.Add(node.Clone());
        }
        return tree;
    }
}
