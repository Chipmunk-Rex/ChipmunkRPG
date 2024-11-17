using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponItem : Item, IInteractableItem
{
    public WeaponItem(BaseItemSO itemSO) : base(itemSO)
    {
    }

    [field: SerializeField] public bool VisualOnInteract { get; private set; }
    public abstract IInteractableItemSO interactableItemSO { get; }


    public abstract void OnBeforeInteract(Entity target);

    public abstract void OnEndInteract(Entity target);

    public abstract void OnInteract(Entity target);
}
