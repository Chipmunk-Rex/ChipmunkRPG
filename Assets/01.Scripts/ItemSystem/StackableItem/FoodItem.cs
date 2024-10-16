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

    public void OnInteract(Entity target)
    {
        
    }

    public void OnBeforeInteract(Entity target)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndInteract(Entity target)
    {
        throw new System.NotImplementedException();
    }


}
