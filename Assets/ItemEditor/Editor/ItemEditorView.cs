using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemEditorView : VisualElement
{
    public ItemEditorView()
    {
    }
    public new class UxmlFactory : UxmlFactory<ItemEditorView, VisualElement.UxmlTraits> { }
    // public Action i
    List<BaseItemSO> itemSOList = new();
    public ScrollView listView;
    public void Initialize()
    {
        listView = this.Q<ScrollView>();
    }
    public void ReFreshView()
    {
        ReFreshData();

        listView.Clear();

        itemSOList.ForEach(item =>
        {
            ItemView itemView = new ItemView();
            itemView.Initialize(item);
        });
    }
    public void ReFreshData()
    {
        itemSOList.Clear();
        AssetDatabase.FindAssets("", new[] { "Assets/ItemEditor/ScriptableObject" }).ToList().ForEach(path =>
        {
            Debug.Log("ㅁ");
            BaseItemSO itemSo = AssetDatabase.LoadAssetAtPath<BaseItemSO>(path);
            if (itemSo != null)
            {
                itemSOList.Add(itemSo);
            }
        });
    }
}
