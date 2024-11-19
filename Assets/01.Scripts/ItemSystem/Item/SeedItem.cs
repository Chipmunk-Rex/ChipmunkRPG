using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedItem : StackableItem, IInteractableItem
{
    SeedSO seedSO;
    public SeedItem(BaseItemSO itemSO) : base(itemSO)
    {
        seedSO = itemSO as SeedSO;
    }

    public IInteractableItemSO interactableItemSO => seedSO;

    public bool VisualOnInteract { get; private set; }

    public void OnBeforeInteract(Entity target)
    {

    }

    public void OnEndInteract(Entity target, bool isCanceled)
    {
        if (isCanceled)
            return;

        IInventoryOwner inventoryOwner = target as IInventoryOwner;
        this.itemCount--;
        if (this.itemCount <= 0)
        {
            inventoryOwner.Inventory.RemoveItem(this);
        }
    }

    public void OnInteract(Entity target)
    {
    }
}
