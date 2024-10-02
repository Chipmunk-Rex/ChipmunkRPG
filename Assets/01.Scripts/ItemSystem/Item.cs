using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public BaseItemSO ItemSO { get; protected set; }
    public Item(BaseItemSO itemSO)
    {
        ItemSO = itemSO;
    }
}
