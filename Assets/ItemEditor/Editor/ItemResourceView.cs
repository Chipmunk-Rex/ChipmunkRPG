using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemResourceView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ItemResourceView, VisualElement.UxmlTraits> { }
    private Dictionary<Type, ItemResourceFold> itemResourceFoldDic = new();
    public Action<BaseItemSO> onCreateItem;
    public ItemResourceView()
    {
        ReloadView();
    }
    public void ReloadView()
    {
        this.Clear();
        CreateFold(typeof(BaseItemSO));
    }
    public void CreateFold(Type type)
    {
        this.Clear();
        itemResourceFoldDic.Clear();

        TypeCache.TypeCollection typeCollect = TypeCache.GetTypesDerivedFrom(type);
        foreach (Type cachedType in typeCollect)
        {
            ItemResourceFold itemResourceFold = new ItemResourceFold();
            itemResourceFold.Initialize(cachedType);
            // this.Add(itemResourceFold);
            itemResourceFoldDic.Add(cachedType, itemResourceFold);
            itemResourceFold.onClick += CreateItem;
        }

        foreach (Type cachedType in typeCollect)
        {
            if (cachedType == type)
            {
                Debug.Log("같네?");
                continue;
            }
            Debug.Log(cachedType.BaseType.ToString() + " ㅡㅑ" + cachedType.ToString());
            if (itemResourceFoldDic.ContainsKey(cachedType.BaseType))
            {
                itemResourceFoldDic[cachedType.BaseType].foldout.Add(itemResourceFoldDic[cachedType]);
            }
            else
            {
                this.Add(itemResourceFoldDic[cachedType]);
            }
        }

    }
    public void CreateItem(Type type)
    {
        BaseItemSO itemSO = ScriptableObject.CreateInstance(type) as BaseItemSO;

        Undo.RegisterCreatedObjectUndo(itemSO, "ItemEditor Create Item");

        SaveItem(itemSO, $"Assets/ItemEditor/ScriptableObject/{type.ToString()}");
        onCreateItem?.Invoke(itemSO);
    }
    private void SaveItem(BaseItemSO itemSO, string path)
    {
        BaseItemSO finedSO = AssetDatabase.LoadAssetAtPath<BaseItemSO>($"{path}.asset");
        int count = 0;
        if (finedSO == null)
        {
            AssetDatabase.CreateAsset(itemSO, $"{path}.asset");
            AssetDatabase.SaveAssets();
            return;
        }
        else
        {
            while (finedSO != null)
            {
                count++;
                finedSO = AssetDatabase.LoadAssetAtPath<BaseItemSO>($"{path} {count}.asset");
            }
        }
        AssetDatabase.CreateAsset(itemSO, $"{path} {count}.asset");
        AssetDatabase.SaveAssets();
    }
}
