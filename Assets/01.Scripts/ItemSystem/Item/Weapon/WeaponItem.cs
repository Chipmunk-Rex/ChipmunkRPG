using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponItem : Item, IInteractableItem
{
    public WeaponItem(BaseItemSO itemSO) : base(itemSO)
    {
    }

    public abstract IInteractableItemSO interactableItemSO { get; }

    public abstract void OnBeforeInteract(EntityCompo target);

    public abstract void OnEndInteract(EntityCompo target);

    public abstract void OnInteract(EntityCompo target);
}
