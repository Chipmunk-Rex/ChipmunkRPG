using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item
{
    public BaseItemSO ItemSO { get; protected set; }
    public Item(BaseItemSO itemSO)
    {
        ItemSO = itemSO;
    }
}
