using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableItemSO
{
    [Tooltip("상호작용 시간")]
    public float InteractDuration { get; }
    [Tooltip("상호작용 대기시간")]
    public float InteractCool { get; }
}
