using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandSO : ShootableItemSO
{
    int mana;
    public override Item CreateItem()
    {
        return new WandItem(this);
    }
}
