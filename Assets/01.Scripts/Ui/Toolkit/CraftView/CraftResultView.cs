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
    VisualElement craftButton;
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

        craftButton = new VisualElement();
        craftButton.name = "CraftButton";
        this.Add(craftButton);

        craftButton.RegisterCallback<MouseUpEvent>(OnClickCraftButton);
    }

    private void OnClickCraftButton(MouseUpEvent evt)
    {
        OnCraftAction?.Invoke(SelectedRecipe);
        //털ㄴ업 해야해
    }

    public void DrawResultView(CraftRecipeSO craftRecipe)
    {
        if (craftRecipe == null)
        {
            this.style.display = DisplayStyle.None;
            return;
        }
        craftResultLabel.text = craftRecipe.resultItem.itemName;
        craftResultDesc.text = craftRecipe.resultItem.itemDesc;
        SetBackgroundImage(craftResultImage, craftRecipe.resultItem.itemSprite);
        // craftButton.image = craftRecipe.resultItem.;
        craftButton.RegisterCallback<MouseUpEvent>(evt => OnCraftAction?.Invoke(craftRecipe));
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
