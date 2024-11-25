using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Craft Recipes Table", menuName = "SO/Craft Recipes Table")]
public class CraftRecipesTableSO : ScriptableObject
{
    public List<CraftRecipeSO> craftRecipes = new();

    public CraftRecipeSO GetMatchRecipe(List<Item> items)
    {
        foreach (CraftRecipeSO recipe in craftRecipes)
        {
            if (recipe.CanCraft(items))
                return recipe;
        }
        return null;
    }
}