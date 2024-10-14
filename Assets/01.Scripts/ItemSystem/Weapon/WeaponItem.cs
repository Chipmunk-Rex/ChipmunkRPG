using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponItem : Item, IInteractableItem
{
    public WeaponItem(BaseItemSO itemSO) : base(itemSO)
    {
    }

    public abstract void BeforeInteract(Entity target);

    public abstract void Interact(Entity target);
}
