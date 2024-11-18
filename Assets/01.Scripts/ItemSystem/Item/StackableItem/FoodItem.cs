using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;

public class FoodItem : StackableItem, IInteractableItem
{
    public FoodItem(BaseItemSO itemSO) : base(itemSO)
    {
        foodSO = itemSO as FoodSO;
    }
    private FoodSO foodSO;
    public IInteractableItemSO interactableItemSO => foodSO;

    [field: SerializeField] public bool VisualOnInteract { get; private set; }

    public void OnBeforeInteract(Entity target)
    {

    }
    public void OnInteract(Entity target)
    {
    }


    public void OnEndInteract(Entity target, bool isCanceled)
    {
        if (isCanceled)
            return;
        target.meters[EnumMeterType.Hunger].Value += foodSO.hunger;

        IInventoryOwner inventoryOwner = target as IInventoryOwner;
        inventoryOwner.Inventory.RemoveItem(this);
    }


}
