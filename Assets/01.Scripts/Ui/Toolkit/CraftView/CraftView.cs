using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CraftView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<CraftView, UxmlTraits> { }
    public ItemCrafter ItemCrafter { get; private set; }
    ListView craftRecipeListView;
    CraftResultView craftResultView;
    public CraftView()
    {
        {
            VisualElement craftRecipeView = new VisualElement();
            craftRecipeView.name = "CraftRecipeView";
            this.Add(craftRecipeView);
            {
                craftRecipeListView = new ListView();
                craftRecipeListView.name = "CraftRecipeListView";
                craftRecipeView.Add(craftRecipeListView);
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
            this.Add(craftResultView);
        }
    }
    public void OpenView(ItemCrafter itemCrafter)
    {
        Debug.Log("CraftView OpenView");
        this.style.display = DisplayStyle.Flex;
        this.ItemCrafter = itemCrafter;

        GenarateCraftRecipeView();
    }

    private void GenarateCraftRecipeView()
    {
        craftRecipeListView.hierarchy.Clear();
        foreach (CraftRecipeSO craftRecipe in ItemCrafter.IItemCrafterSO.CraftRecipesTable.craftRecipes)
        {
            var craftRecipeViewItem = new CraftRecipeViewItem(craftRecipe);
            craftRecipeListView.hierarchy.Add(craftRecipeViewItem);
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
        if (this.ItemCrafter != null)
            this.ItemCrafter.Close();
        this.ItemCrafter = null;
    }
}
