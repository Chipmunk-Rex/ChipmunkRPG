using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftRecipeViewItem : VisualElement
{
    public Action<CraftRecipeViewItem> OnClickAction;
    public CraftRecipeSO CraftRecipe { get; private set; }
    public CraftRecipeViewItem(CraftRecipeSO craftRecipe)
    {
        {
            var image = new VisualElement();
            image.name = "ItemImage";
            SetBackgroundImage(image, craftRecipe.resultItemSO.itemSprite);
            this.Add(image);
        }

        {
            Label title = new Label(craftRecipe.resultItemSO.itemName);
            title.name = "ItemName";
            this.Add(title);

            this.CraftRecipe = craftRecipe;
        }

        RegisterCallback<ClickEvent>(OnClick);
    }

    private void OnClick(ClickEvent evt)
    {
        OnClickAction?.Invoke(this);
    }

    public static void SetBackgroundImage(VisualElement element, Sprite sprite)
    {
        StyleBackground backgroundImage = element.style.backgroundImage;
        Background background = backgroundImage.value;
        background.sprite = sprite;
        backgroundImage.value = background;
        element.style.backgroundImage = backgroundImage;
    }
}
