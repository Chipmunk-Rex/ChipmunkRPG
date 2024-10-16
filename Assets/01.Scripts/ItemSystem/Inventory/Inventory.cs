using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ItemContainer
{
    #region getter
    public InventoryHotbar Hotbar => hotbar;
    #endregion
    [SerializeField] private InventoryHotbar hotbar;
}
