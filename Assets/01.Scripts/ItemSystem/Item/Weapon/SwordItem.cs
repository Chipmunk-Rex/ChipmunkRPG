using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItem : WeaponItem
{
    public SwordItem(BaseItemSO itemSO) : base(itemSO)
    {
    }

    public override IInteractableItemSO interactableItemSO => throw new System.NotImplementedException();

    public override void OnBeforeInteract(EntityCompo target)
    {
    }

    public override void OnEndInteract(EntityCompo target)
    {
        throw new System.NotImplementedException();
    }

    public override void OnInteract(EntityCompo target)
    {

    }
}
