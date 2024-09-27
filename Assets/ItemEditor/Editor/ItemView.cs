using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemView : VisualElement
{
    public BaseItemSO itemSO { get; private set; }
    VisualElement rootElement;
    VisualElement itemSpriteElement;
    Label itemName;
    Label itemRarity;
    Button deleteBtn;
    public ItemView()
    {
        rootElement = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/ItemEditor/Editor/ItemView.uxml").Instantiate().Q<VisualElement>("ItemView");
        this.Add(rootElement);

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/ItemEditor/Editor/ItemView.uss");
        this.styleSheets.Add(styleSheet);

        itemSpriteElement = rootElement.Q("ItemSprite");
        itemName = rootElement.Q<Label>("ItemName");
        itemRarity = rootElement.Q<Label>("ItemRarity");
        deleteBtn = rootElement.Q<Button>("DeleteButton");
    }
    public Action<ItemView> onClick;
    public Action<ItemView> onDeleteButtonClick;
    public void Initialize(BaseItemSO itemSO)
    {
        this.itemSO = itemSO;

        var img = itemSpriteElement.style.backgroundImage;
        Background background = itemSpriteElement.style.backgroundImage.value;
        background.sprite = itemSO.itemSprite;
        img.value = background;
        itemSpriteElement.style.backgroundImage = img;

        itemName.text = itemSO.itemName;

        string rarity = itemSO.enumItemRarity.ToString();
        itemRarity.text = rarity;
        rootElement.AddToClassList(rarity);

        deleteBtn.RegisterCallback<ClickEvent>(evt => OnDeleteClick());
        RegisterCallback<ClickEvent>(evt => OnClick());
    }

    private void OnDeleteClick()
    {
        Debug.Log("click");
        onDeleteButtonClick.Invoke(this);
    }

    public void AddClass(string className)
    {
        rootElement.AddToClassList(className);
    }
    public void RemoveClass(string className)
    {
        rootElement.RemoveFromClassList(className);
    }

    private void OnClick()
    {
        onClick?.Invoke(this);
    }
}
