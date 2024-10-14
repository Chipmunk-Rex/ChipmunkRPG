using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingItem : StackableItem, IInteractableItem
{
    public BuildingItem(BaseItemSO itemSO) : base(itemSO)
    {
    }

    public void Interact(Entity target)
    {
    }
}
