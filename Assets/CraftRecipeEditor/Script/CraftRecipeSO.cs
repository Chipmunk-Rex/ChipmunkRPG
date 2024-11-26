using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CraftRecipe", menuName = "SO/CraftRecipeSO", order = 1)]
[System.Serializable]
public class CraftRecipeSO : ScriptableObject
{
    private Dictionary<BaseItemSO, int> requireIngredients;
    public Dictionary<BaseItemSO, int> RequireIngredients
    {
        get
        {
            if (requireIngredients == null)
            {
                requireIngredients = new Dictionary<BaseItemSO, int>();

                foreach (CraftRecipeIngrdient ingredient in IngredientItems)
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
    public List<CraftRecipeIngrdient> IngredientItems = new();
    public BaseItemSO resultItemSO;
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

        if (recipeItems == null || recipeItems.Count == 0 && IngredientItems.Count != 0)
            return false;

        foreach (Item item in recipeItems)
        {
            if (item == null)
            {
                Debug.LogWarning("Item is null");
                continue;
            }
            if (recipeSOs.ContainsKey(item.ItemSO))
                recipeSOs[item.ItemSO] += 1;
            else
                recipeSOs.Add(item.ItemSO, item.StackCount);
        }

        return CanCraft(recipeSOs);
    }
}
[System.Serializable]
public class CraftRecipeIngrdient
{
    [field: SerializeField] public BaseItemSO Ingredient { get; private set; }
    public int count;
}