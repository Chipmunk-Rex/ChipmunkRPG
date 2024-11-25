using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CraftRecipe", menuName = "SO/CraftRecipeSO", order = 1)]
[System.Serializable]
public class CraftRecipeSO : ScriptableObject
{
    public Dictionary<BaseItemSO, int> requireIngredients;
    public Dictionary<BaseItemSO, int> RequireIngredients
    {
        get
        {
            if (requireIngredients == null)
        {
                requireIngredients = new Dictionary<BaseItemSO, int>();

                foreach (CraftRecipeIngrdient ingredient in Ingredients)
                {
                    if (requireIngredients.ContainsKey(ingredient.Ingredient))
                        requireIngredients[ingredient.Ingredient] = ingredient.count;
                    else
                        requireIngredients.Add(ingredient.Ingredient, ingredient.count);
                }
            }

            return requireIngredients;
        }
    }
    public List<CraftRecipeIngrdient> Ingredients = new();
    public BaseItemSO resultItem;
    public int resultCount;

    public bool CanCraft(Dictionary<BaseItemSO, int> recipeItems)
    {
        Dictionary<BaseItemSO, int> requireIngrdients = RequireIngredients;


        bool canCraft = true;
        foreach (KeyValuePair<BaseItemSO, int> keyValuePair in recipeItems)
        {
            if (requireIngrdients.ContainsKey(keyValuePair.Key))
                if (keyValuePair.Value > requireIngrdients[keyValuePair.Key])
                {
                    canCraft = false;
                }
        }

        return canCraft;
    }

    public bool CanCraft(List<Item> recipeItems)
    {
        Dictionary<BaseItemSO, int> recipeSOs = new Dictionary<BaseItemSO, int>();
        foreach (Item item in recipeItems)
        {
            if (item is StackableItem stackableItem)
                recipeSOs.Add(item.ItemSO, stackableItem.itemCount);
            else
            if (recipeSOs.ContainsKey(item.ItemSO))
                recipeSOs[item.ItemSO] += 1;
            else
                recipeSOs.Add(item.ItemSO, 1);
        }

        return CanCraft(recipeSOs);
    }
}
public struct CraftRecipeIngrdient
{
    public BaseItemSO Ingredient { get; private set; }
    public int count;
}