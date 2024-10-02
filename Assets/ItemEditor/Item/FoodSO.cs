using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSO : StackableItemSO, IInteractableItemSO
{
    public int hunger = 1;

    [field: SerializeField] public float InteractDuration { get; private set; }

    [field: SerializeField] public float InteractCool { get; private set; }

    public override Item CreateItem()
    {
        Item item = new Item(this);
        return item;
    }
}
