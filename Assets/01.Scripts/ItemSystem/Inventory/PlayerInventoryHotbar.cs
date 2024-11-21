using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHotbar : InventoryHotbar
{
    public PlayerInventoryHotbar(Entity owner, Inventory inventory, int hotbarSize) : base(owner, inventory, hotbarSize)
    {
        PlayerInputReader.Instance.onItemUse += UseItem;
    }

    private void UseItem(bool value)
    {
        if (value)
            UseItem(this.GetSelectedItem() as IInteractableItem);
        else
            UseItem(null);
    }
}
