using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : ItemSO, IInteractableItemSO
{
    public int damage = 1;

    [field: SerializeField] public float InteractDuration { get; private set; }

    [field: SerializeField] public float InteractCool { get; private set; }
}
