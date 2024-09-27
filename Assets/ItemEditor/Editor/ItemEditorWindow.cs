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
    VisualElement reloadBtn;

    [MenuItem("Window/ItemWindow")]
    public static void OnOpenWindow()
    {
        ItemEditorWindow window = GetWindow<ItemEditorWindow>();
        window.titleContent = new GUIContent("ItemEditor");

    }

    private void OnUndoRedo()
    {
        OnReload();
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

        itemResourceView.onCreateItem += OnSelectItem;
        itemResourceView.onCreateItem += itemEditorView.ReFreshViewAndSelect;
        itemEditorView.Initialize();
        itemEditorView.onSelectItem += OnSelectItem;
        itemInspectorView.onDataChange += OnDataChange;



        reloadBtn = rootEle.Q("ReloadBtn");
        reloadBtn.RegisterCallback<ClickEvent>(OnReload);

        Undo.undoRedoPerformed += OnUndoRedo;
    }

    private void OnDataChange()
    {
        itemEditorView.ReFreshView();
    }

    private void OnReload(ClickEvent evt)
    {
        Debug.Log("mingming이 왔어요~!");
        OnReload();
    }
    private void OnReload()
    {
        itemEditorView.ReFreshView();
        itemResourceView.ReloadView();
    }
    public void OnSelectItem(BaseItemSO baseItemSO)
    {
        itemInspectorView.UpdateInspactor(baseItemSO);
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
