using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EntityListViewItem : VisualElement
{
    public EntitySO entitySO { get; private set; }

    private VisualElement SpriteVisual;
    private Label NameLabel;
    Action<EntityListViewItem> onClick;
    public EntityListViewItem()
    {
        RegisterCallback<ClickEvent>(OnClick);
    }

    private void OnClick(ClickEvent evt)
    {
        onClick?.Invoke(this);
    }
    public void DrawView(EntitySO entitySO, Action<EntityListViewItem> onClick)
    {
        this.onClick = onClick;
        this.entitySO = entitySO;
        CreateElement();

        if (entitySO.defaultSprite != null)
            SpriteVisual.style.backgroundImage = entitySO.defaultSprite.texture;
    }

    private void CreateElement()
    {
        SpriteVisual = new VisualElement();
        SpriteVisual.name = "SpriteVisual";
        this.Add(SpriteVisual);

        NameLabel = new Label(entitySO.entityName);
        NameLabel.name = "Name";
        this.Add(NameLabel);
    }

    internal void SetSelected(bool value)
    {
        if(value)
        {
            AddToClassList("Selected");
        }
        else
        {
            RemoveFromClassList("Selected");
        }
    }
}
