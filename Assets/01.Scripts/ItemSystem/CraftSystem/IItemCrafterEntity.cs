using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemCrafterEntity
{
    public Transform Transform { get; }
    IItemCrafterSO ItemCrafterSO { get; }
    public ItemCrafter ItemCrafter { get; }
}
