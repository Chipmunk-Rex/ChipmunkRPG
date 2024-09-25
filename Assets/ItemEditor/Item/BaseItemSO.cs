using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemSO : ScriptableObject
{
    [SerializeField] public string itemName = "Item";
    [SerializeField] public Sprite itemSprite;
    [SerializeField] public int maxStackCount;
    public EnumItemRarity enumItemRarity = EnumItemRarity.None;
    public abstract void Initialize();
}
