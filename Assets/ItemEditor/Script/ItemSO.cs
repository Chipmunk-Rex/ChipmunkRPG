using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSO : BaseItemSO
{
    public override Item CreateItem()
    {
        Item item = new Item(this);
        return item;
    }
}