using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItemSO : ScriptableObject
{
    [SerializeField] public string itemName = "item";
    [SerializeField] public string itemDesc = "item description";
    [SerializeField] public Sprite itemSprite;

    public int maxStackCount = 64;
    // [SerializeField] public int maxStackCount = 64; // Stackable Item을 따로 만듬
    public EnumItemRarity enumItemRarity = EnumItemRarity.None;
    /// <summary>
    /// 내구도
    /// </summary>
    [Tooltip("내구도(미구현)")]
    public int Durability;
    public abstract Item CreateItem();
}
