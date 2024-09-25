using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemView : VisualElement
{
    BaseItemSO itemSO;
    public ItemView()
    {
        VisualElement element = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/ItemEditor/Editor/ItemView.uxml").Instantiate();
        this.Add(element);
    }
    public void Initialize(BaseItemSO itemSO)
    {
        this.itemSO = itemSO;

        VisualElement itemSprite = this.Q("");

        RegisterCallback<ClickEvent>(evt => OnMouseClick(evt));
    }

    private void OnMouseClick(ClickEvent evt)
    {
        // this.Add()
    }
}
