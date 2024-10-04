using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableItem : Item
{
    public int itemCount;

    public StackableItem(BaseItemSO itemSO) : base(itemSO)
    {
    }
}
