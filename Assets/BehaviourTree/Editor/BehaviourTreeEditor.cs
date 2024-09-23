using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class BehaviourTreeEditor : EditorWindow
{
    BehaviourTreeView treeView;
    BT_InspectorView inspectorView;
    [MenuItem("Window/BehaviourTree")]
    public static void OpenWindow()
    {
        BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviourTreeEditor");
    }
    [OnOpenAsset]
    public static bool OnOpenAsset(int instanceID, int line)
    {
        BT_BehaviourTree tree = EditorUtility.InstanceIDToObject(instanceID) as BT_BehaviourTree;
        if (tree != null)
        {
            OpenWindow();
            return true;
        }
        return false;
    }
    public void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/BehaviourTree/Editor/BehaviourTreeEditor.uxml");
        visualTree.CloneTree(root);

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BehaviourTree/Editor/BehaviourTreeEditor.uss");
        root.styleSheets.Add(styleSheet);

        treeView = root.Q<BehaviourTreeView>();
        treeView.onNodeSeleted += OnNodeSeletionChange;
        inspectorView = root.Q<BT_InspectorView>();

        OnSelectionChange();
    }

    private void OnNodeSeletionChange(BT_NodeView nodeView)
    {
        inspectorView.UpdateSelection(nodeView);
    }

    private void OnSelectionChange()
    {
        BT_BehaviourTree tree = Selection.activeObject as BT_BehaviourTree;
        if (tree != null && AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
        {
            treeView.PopulateView(tree);
        }
    }
}
