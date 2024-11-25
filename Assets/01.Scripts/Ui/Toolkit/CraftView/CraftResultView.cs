using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftResultView : VisualElement
{
    Label craftResultLabel;
    Label craftResultDesc;
    VisualElement craftResultImage;
    Label craftButton;
    public Action<CraftRecipeSO> OnCraftAction;
    public CraftRecipeSO SelectedRecipe { get; private set; }
    public CraftResultView()
    {
        {
            craftResultLabel = new Label("Craft Result Label");
            craftResultLabel.name = "ItemName";
            this.Add(craftResultLabel);


            craftResultImage = new VisualElement();
            craftResultImage.name = "CraftResultImage";
            this.Add(craftResultImage);

            craftResultDesc = new Label("Craft Result Desc");
            craftResultDesc.name = "ItemDesc";
            this.Add(craftResultDesc);
        }

        craftButton = new Label("Craft");
        craftButton.name = "CraftButton";
        this.Add(craftButton);

        craftButton.RegisterCallback<MouseUpEvent>(OnClickCraftButton);
    }

    private void OnClickCraftButton(MouseUpEvent evt)
    {
        OnCraftAction?.Invoke(SelectedRecipe);
    }

    public void DrawResultView(CraftRecipeSO craftRecipe)
    {
        if (craftRecipe == null)
        {
            this.style.display = DisplayStyle.None;
            return;
        }
        this.SelectedRecipe = craftRecipe;
        craftResultLabel.text = craftRecipe.resultItemSO.itemName;
        craftResultDesc.text = craftRecipe.resultItemSO.itemDesc;
        SetBackgroundImage(craftResultImage, craftRecipe.resultItemSO.itemSprite);
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
