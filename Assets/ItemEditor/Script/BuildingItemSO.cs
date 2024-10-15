using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingItemSO : StackableItemSO, IInteractableItemSO
{
    public BuildingSO buildingSO;

    [field: SerializeField] public float InteractDuration { get; private set; }

    [field: SerializeField] public float InteractCool { get; private set; }

    public override Item CreateItem()
    {
        throw new System.NotImplementedException();
    }
}
