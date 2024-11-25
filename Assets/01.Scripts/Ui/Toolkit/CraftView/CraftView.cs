using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CraftView, UxmlTraits> { }
    public ItemCrafter ItemCrafter { get; private set; }
    public ItemContainer ItemContainer { get; private set; }
    ScrollView craftRecipeScrollView;
    CraftResultView craftResultView;
    public CraftView()
    {
        {
            VisualElement craftRecipeView = new VisualElement();
            craftRecipeView.name = "CraftRecipeView";
            this.Add(craftRecipeView);
            {
                craftRecipeScrollView = new ScrollView(ScrollViewMode.Vertical);
                craftRecipeScrollView.name = "CraftRecipeListView";
                craftRecipeScrollView.verticalScrollerVisibility = ScrollerVisibility.Hidden;
                craftRecipeScrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
                craftRecipeScrollView.mouseWheelScrollSize = 40;
                craftRecipeView.Add(craftRecipeScrollView);
            }
            {
                VisualElement craftViewTitle = new VisualElement();
                craftViewTitle.name = "CraftViewTitle";
                craftRecipeView.Add(craftViewTitle);

                Label craftViewLbl = new Label("Craft");
                craftViewLbl.name = "Text";
                craftViewTitle.Add(craftViewLbl);
            }
        }
        {
            craftResultView = new CraftResultView();
            craftResultView.OnCraftAction += CraftItem;
            this.Add(craftResultView);
        }
    }

    private void CraftItem(CraftRecipeSO sO)
    {
        ItemCrafter.Craft(ItemContainer, sO);
    }

    public void OpenView(ItemCrafter itemCrafter, ItemContainer itemContainer)
    {
        this.style.display = DisplayStyle.Flex;
        this.ItemCrafter = itemCrafter;
        this.ItemContainer = itemContainer;

        GenarateCraftRecipeView();
    }

    private void GenarateCraftRecipeView()
    {
        craftRecipeScrollView.Clear();
        foreach (CraftRecipeSO craftRecipe in ItemCrafter.IItemCrafterSO.CraftRecipesTable.craftRecipes)
        {
            var craftRecipeViewItem = new CraftRecipeViewItem(craftRecipe);
            craftRecipeScrollView.Add(craftRecipeViewItem);
            craftRecipeViewItem.OnClickAction += OnCraftRecipeClick;
        }
    }

    private void OnCraftRecipeClick(CraftRecipeViewItem item)
    {
        craftResultView.DrawResultView(item.CraftRecipe);
    }

    public void CloseView()
    {
        Debug.Log("CraftView CloseView");
        this.style.display = DisplayStyle.None;
        // if (this.ItemCrafter != null)
        //     this.ItemCrafter.Close();
        this.ItemCrafter = null;
    }
}
