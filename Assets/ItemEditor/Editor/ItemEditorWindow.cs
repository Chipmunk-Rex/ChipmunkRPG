using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ItemEditorWindow : EditorWindow
{
    ItemSplitView itemSplitView;
    ItemInspectorView itemInspectorView;
    ItemEditorView itemEditorView;
    ItemResourceView itemResourceView;

    [MenuItem("Window/ItemWindow")]
    public static void OnOpenWindow()
    {
        ItemEditorWindow window = GetWindow<ItemEditorWindow>();
        window.titleContent = new GUIContent("ItemEditor");
    }

    public void CreateGUI()
    {
        VisualElement rootEle = rootVisualElement;

        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/ItemEditor/Editor/ItemEditorWindow.uxml");
        visualTree.CloneTree(rootEle);

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/ItemEditor/Editor/ItemEditorWindow.uss");
        rootEle.styleSheets.Add(styleSheet);

        itemSplitView = rootEle.Q<ItemSplitView>();

        itemInspectorView = QuearyOrAction<ItemInspectorView>(itemSplitView, () => new ItemInspectorView());
        itemResourceView = QuearyOrAction<ItemResourceView>(itemSplitView, () => new ItemResourceView());
        itemEditorView = QuearyOrAction<ItemEditorView>(itemSplitView, () => new ItemEditorView());
        
        // rootEle.Add(itemInspectorView);

        itemEditorView.Initialize();
        itemEditorView.ReFreshView();
    }
    public static T QuearyOrAction<T>(VisualElement root, Func<T> action) where T : VisualElement
    {
        T find = root.Q<T>();
        if (find == null)
            find = action.Invoke();
        if (root.Q<T>() == null)
            root.Add(find);
        return find;
    }
}
