using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSO : StackableItemSO, IInteractableItemSO
{
    [field: SerializeField]public float InteractDuration { get; private set;}

    [field: SerializeField]public float InteractCool { get; private set;}
    [Tooltip("씨앗을 심었을때 나오는 식물의 SO")]
    public BuildingSO plantSO;
    public override Item CreateItem()
    {
        return new SeedItem(this);
    }
}
