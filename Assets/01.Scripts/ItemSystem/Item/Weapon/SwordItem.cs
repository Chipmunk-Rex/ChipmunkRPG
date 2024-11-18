using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItem : WeaponItem
{
    public SwordItem(BaseItemSO itemSO) : base(itemSO)
    {
    }

    public override IInteractableItemSO interactableItemSO => throw new System.NotImplementedException();

    public override void OnBeforeInteract(Entity target)
    {
    }

    public override void OnEndInteract(Entity target, bool isCanceled)
    {
        throw new System.NotImplementedException();
    }

    public override void OnInteract(Entity target)
    {

    }
}
