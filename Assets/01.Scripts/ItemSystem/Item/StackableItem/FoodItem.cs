using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : StackableItem, IInteractableItem
{
    public FoodItem(BaseItemSO itemSO) : base(itemSO)
    {
        foodSO = itemSO as FoodSO;
    }
    private FoodSO foodSO;
    public IInteractableItemSO interactableItemSO => foodSO;

    public void OnInteract(EntityCompo target)
    {
        
    }

    public void OnBeforeInteract(EntityCompo target)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndInteract(EntityCompo target)
    {
        throw new System.NotImplementedException();
    }


}
