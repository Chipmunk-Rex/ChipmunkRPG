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

    public StackableItem(BaseItemSO itemSO) : base(itemSO)
    {
    }
}
