using System.Collections;
using System.Collections.Generic;

using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEditor;
using System;

public class BehaviourTreeView : GraphView
{
    public new class UxmlFactory : UxmlFactory<BehaviourTreeView, GraphView.UxmlTraits> { }
    BT_BehaviourTree tree;
    public BehaviourTreeView()
    {
        Insert(0, new GridBackground() { name = "GridBackground" });

        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BehaviourTree/Editor/BehaviourTreeEditor.uss");
        styleSheets.Add(styleSheet);
    }
    public void PopulateView(BT_BehaviourTree tree)
    {
        this.tree = tree;

        DeleteElements(graphElements);

        this.tree.nodes.ForEach(n => CreateNodeView(n));
        Debug.Log(this.tree != null);
    }

    private void CreateNodeView(BT_Node n)
    {
        BT_NodeView nodeView = new BT_NodeView(n);
        AddElement(nodeView);
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        // base.BuildContextualMenu(evt);

        TypeCache.TypeCollection types = TypeCache.GetTypesDerivedFrom<BT_ActionNode>();
        foreach (Type type in types)
        {
            evt.menu.AppendAction($"{type.BaseType.Name}/{type.Name}", (a) => { CreateNode(type); });
        }
    }

    private void CreateNode(Type type)
    {
        Debug.Log(tree != null);
        BT_Node node = tree.CreateNode(type);
        CreateNodeView(node);
    }
}
