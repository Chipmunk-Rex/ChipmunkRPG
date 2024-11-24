using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CraftRecipe", menuName = "SO/CraftRecipeSO", order = 1)]
[System.Serializable]
public class CraftRecipeSO : ScriptableObject
{
    public List<BaseItemSO> Ingredients = new();
    public BaseItemSO ResultItem;
    public int ResultCount;

    public bool IsMatch(List<BaseItemSO> recipeItems)
    {
        if (recipeItems.Count != Ingredients.Count)
            return false;
        for (int i = 0; i < recipeItems.Count; i++)
        {
            if (recipeItems[i] != Ingredients[i])
                return false;
        }

        return true;
    }
    public bool IsMatch(List<Item> recipeItems)
    {
        if (recipeItems.Count != Ingredients.Count)
            return false;
        for (int i = 0; i < recipeItems.Count; i++)
        {
            if (recipeItems[i].ItemSO != Ingredients[i])
                return false;
        }

        return true;
    }
}