using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemSO : ScriptableObject
{
    [SerializeField] public string itemName = "item";
    [SerializeField] public Sprite itemSprite;
    [SerializeField] public int maxStackCount = 64;
    public EnumItemRarity enumItemRarity = EnumItemRarity.None;
    public abstract void Initialize();
}
