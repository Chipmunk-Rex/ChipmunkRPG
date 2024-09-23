using System.Collections;
using System.Collections.Generic;

using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class BehaviourTreeView : GraphView
{
    public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { }
    BT_BehaviourTree tree;
    public Action<BT_NodeView> onNodeSeleted;
    public BehaviourTreeView()
    {
        Insert(0, new GridBackground() { name = "GridBackground" });

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BehaviourTree/Editor/BehaviourTreeEditor.uss");
        styleSheets.Add(styleSheet);

        Undo.undoRedoPerformed += OnUndoRedo;
    }

    private void OnUndoRedo()
    {
        Debug.Log("UndoRedo");
        AssetDatabase.SaveAssets();
        EditorUtility.SetDirty(tree);
        PopulateView(tree);
    }

    public void PopulateView(BT_BehaviourTree tree)
    {
        this.tree = tree;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;
        Debug.Log("populate");

        if (tree.rootNode == null)
        {
            tree.rootNode = tree.CreateNode(typeof(BT_RootNode)) as BT_RootNode;
            EditorUtility.SetDirty(tree);
            AssetDatabase.SaveAssets();
        }
        this.tree.nodes.ForEach(n => { Debug.Log(n == null); CreateNodeView(n); });

        this.tree.nodes.ForEach(parentNode =>
        {
            List<BT_Node> childrenNode = tree.GetChildren(parentNode);
            BT_NodeView parentNodeView = FindNodeView(parentNode);
            childrenNode.ForEach(childNode =>
            {
                BT_NodeView childNodeView = FindNodeView(childNode);

                Edge edge = parentNodeView.output.ConnectTo(childNodeView.input);
                AddElement(edge);
            });
        });
    }

    private BT_NodeView FindNodeView(BT_Node node)
    {
        if (node == null)
            Debug.Log("큰일이야2");
        var varNode = GetNodeByGuid(node.guid);
        if (varNode == null)
        {
            Debug.Log("큰일이야");
        }
        return varNode as BT_NodeView;
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort =>
            endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(element =>
            {
                BT_NodeView nodeView = element as BT_NodeView;
                if (nodeView != null)
                    tree.DeleteNode(nodeView.node);

                Edge edge = element as Edge;
                if (edge != null)
                {
                    BT_NodeView parentView = edge.output.node as BT_NodeView;
                    BT_NodeView childView = edge.input.node as BT_NodeView;
                    tree.RemoveChildNode(parentView.node, childView.node);
                }
            });
        }

        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                BT_NodeView parentView = edge.output.node as BT_NodeView;
                BT_NodeView childView = edge.input.node as BT_NodeView;
                tree.AddChildNode(parentView.node, childView.node);
            });
        }

        return graphViewChange;
    }

    private void CreateNodeView(BT_Node n)
    {
        if (n == null)
            Debug.LogWarning("큰일이야");
        BT_NodeView nodeView = new BT_NodeView(n);
        nodeView.onNodeSeleted += onNodeSeleted;
        AddElement(nodeView);
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {

        TypeCache.TypeCollection baseTypes = TypeCache.GetTypesDerivedFrom<BT_Node>();
        foreach (Type baseType in baseTypes)
        {
            TypeCache.TypeCollection typeCollection = TypeCache.GetTypesDerivedFrom(baseType);
            foreach (Type type in typeCollection)
            {
                string baseName = BT_NodeView.NodeNameCreator(type.BaseType.Name);
                string name = BT_NodeView.NodeNameCreator(type.Name);
                evt.menu.AppendAction($"{baseName}/{name}", (a) => { CreateNode(type); });
            }
        }
        base.BuildContextualMenu(evt);
    }

    private void CreateNode(Type type)
    {
        BT_Node node = tree.CreateNode(type);
        CreateNodeView(node);
    }
}
