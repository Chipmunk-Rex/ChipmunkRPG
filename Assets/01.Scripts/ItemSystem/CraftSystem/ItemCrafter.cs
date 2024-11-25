using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCrafter
{
    IItemCrafterEntity IItemCrafter { get; set; }
    public IItemCrafterSO IItemCrafterSO => IItemCrafter.ItemCrafterSO;

    public ItemCrafter(IItemCrafterEntity IItemCrafter)
    {
        this.IItemCrafter = IItemCrafter;
    }
    public void Close()
    {

    }
}
