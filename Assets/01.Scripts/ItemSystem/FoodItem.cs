using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : Item, IInteractableItem
{
    public void Interact(Entity target)
    {
        
    }
    public FoodItem(BaseItemSO itemSO) : base(itemSO)
    {
    }

}
