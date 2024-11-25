using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemCrafter
{
    IItemCrafterEntity IItemCrafter { get; set; }
    public IItemCrafterSO IItemCrafterSO => IItemCrafter.ItemCrafterSO;

    public ItemCrafter(IItemCrafterEntity IItemCrafter)
    {
        this.IItemCrafter = IItemCrafter;
    }
    public void Craft(ItemContainer itemContainer, CraftRecipeSO craftRecipeSO)
    {
        List<Item> items = itemContainer.Items.ToList();
        if (craftRecipeSO.CanCraft(items))
        {
            // 차감
            foreach (KeyValuePair<BaseItemSO, int> itemValuePair in craftRecipeSO.RequireIngredients)
            {
                foreach (Item item in items)
                {
                    if (item.ItemSO == itemValuePair.Key)
                    {
                        itemContainer.RemoveItem(item, itemValuePair.Value);
                    }
                }
            }
            // 지급
            Item resultItem = craftRecipeSO.resultItemSO.CreateItem();
            for (int i = 0; i < craftRecipeSO.resultCount; i++)
            {
                resultItem = craftRecipeSO.resultItemSO.CreateItem();
                itemContainer.AddItem(resultItem);
            }
        }

    }
}
