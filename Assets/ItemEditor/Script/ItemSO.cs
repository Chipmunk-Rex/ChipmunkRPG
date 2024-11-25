using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSO : BaseItemSO
{
    public int maxStackCount = 64;
    public override Item CreateItem()
    {
        Item item = new Item(this);
        return item;
    }
}
