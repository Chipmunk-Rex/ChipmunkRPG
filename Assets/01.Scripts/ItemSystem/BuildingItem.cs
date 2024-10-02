using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingItem : Item, IInteractableItem
{
    public BuildingItem(BaseItemSO itemSO) : base(itemSO)
    {
    }

    public void Interact(Entity target)
    {
    }
}
