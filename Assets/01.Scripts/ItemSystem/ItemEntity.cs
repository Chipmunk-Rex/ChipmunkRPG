using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
using UnityEngine;
public class ItemEntity : Entity, IPoolAble
{
    public string PoolName => "ItemEntity";
    public GameObject ObjectPref => gameObject;
    public Item item { get; private set; }

    public void Initialize(Item item)
    {
        this.item = item;
        gameObject.name = $"Item ({item.ItemSO.itemName})";
        spriteRendererCompo.sprite = item.ItemSO.itemSprite;
    }

    public void InitializeItem()
    {

    }

    public void ResetItem()
    {
        item = null;
        spriteRendererCompo.sprite = null;
    }
}
