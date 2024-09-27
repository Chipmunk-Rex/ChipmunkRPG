using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableItemSO : ItemSO
{
    [Tooltip("상호작용 시간")]
    public float interactDuration = 0.5f;
    [Tooltip("상호작용 대기시간")]
    public float interactCool = 3;
}
