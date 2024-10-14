using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingItem : StackableItem, IInteractableItem
{
    public BuildingItem(BaseItemSO itemSO) : base(itemSO)
    {
    }

    public void BeforeInteract(Entity target)
    {
        throw new System.NotImplementedException();
    }

    public void Interact(Entity target)
    {
    }
}
