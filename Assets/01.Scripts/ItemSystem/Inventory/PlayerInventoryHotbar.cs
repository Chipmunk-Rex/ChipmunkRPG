using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHotbar : InventoryHotbar
{
    protected override void Awake()
    {
        base.Awake();
        PlayerInputReader.Instance.onItemUse += UseItem;
    }

    private void UseItem(bool value)
    {
        Debug.Log(value);
        if (value)
            this.targetUseItem = this.GetSelectedItem() as IInteractableItem;
        else
            this.targetUseItem = null;
    }
}
