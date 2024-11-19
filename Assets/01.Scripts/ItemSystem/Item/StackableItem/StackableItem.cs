using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StackableItem : Item
{
    public int itemCount;
    public int ItemCount
    {
        get => itemCount;
        set
        {
            itemCount = value;
            if (itemCount <= 0)
            {
                itemCount = 0;
            }
        }
    }
    public void RemoveStack(Entity target)
    {
        IInventoryOwner inventoryOwner = target as IInventoryOwner;
        this.itemCount--;
        if (this.itemCount <= 0)
        {
            inventoryOwner.Inventory.RemoveItem(this);
        }
    }

    public StackableItem(BaseItemSO itemSO) : base(itemSO)
    {
    }
}
