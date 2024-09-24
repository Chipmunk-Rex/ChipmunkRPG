using System;
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

        Undo.RegisterCompleteObjectUndo(this, "BehaviourTreeaa Create Node");
        nodes.Add(node);

        if (!Application.isPlaying)
        {
            AssetDatabase.AddObjectToAsset(node, this);

        }

        // Undo.RegisterCreatedObjectUndo(node, "BehaviourTree Create Node");
        AssetDatabase.SaveAssets();
        return node;
    }
    public void DeleteNode(BT_Node node)
    {
        Undo.RecordObject(this, "BehaviourTree Delete Node");
        nodes.Remove(node);
        Undo.DestroyObjectImmediate(node);
        // AssetDatabase.RemoveObjectFromAsset(node);
        AssetDatabase.SaveAssets();
    }
    public void AddChildNode(BT_Node parent, BT_Node child)
    {
        Undo.RecordObject(parent, "BehaviourTree Add Child Node");
        parent.AddChild(child);
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
    public void TraveNode(BT_Node node, Action<BT_Node> onVisit)
    {
        if (node != null)
        {
            onVisit.Invoke(node);
            List<BT_Node> children = node.GetChild();
            children.ForEach((childNode) => TraveNode(childNode, onVisit));
        }
    }
    public BT_BehaviourTree Clone()
    {
        BT_BehaviourTree tree = Instantiate(this);
        tree.rootNode = rootNode.Clone() as BT_RootNode;
        tree.nodes = new List<BT_Node>();
        TraveNode(tree.rootNode, (node) =>
        {
            tree.nodes.Add(node);
        });
        return tree;
    }
}
