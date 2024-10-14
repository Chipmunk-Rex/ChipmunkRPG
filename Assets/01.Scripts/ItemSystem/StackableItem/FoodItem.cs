using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : StackableItem, IInteractableItem
{
    public void Interact(Entity target)
    {
        
    }

    public void BeforeInteract(Entity target)
    {
        throw new System.NotImplementedException();
    }

    public FoodItem(BaseItemSO itemSO) : base(itemSO)
    {
    }

}
