using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ItemSO")]
public class ItemSO : BaseItemSO
{
    public override Item CreateItem()
    {
        return new Item(this);
    }
}
