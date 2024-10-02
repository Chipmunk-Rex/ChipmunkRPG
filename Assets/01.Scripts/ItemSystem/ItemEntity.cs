using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
using UnityEngine;
public class ItemEntity : Entity, IPoolAble
{
    SpriteRenderer spriteRenderer;
    public string PoolName => "ItemEntity";
    public GameObject ObjectPref => gameObject;
    public Item item { get; private set; }

    public void Initialize(Item item)
    {
        this.item = item;

        spriteRenderer.sprite = item.ItemSO.itemSprite;
    }
    protected override void Awake()
    {
        base.Awake();
        GameObject visualObj = new GameObject("Visual");
        visualObj.transform.SetParent(this.transform);
        spriteRenderer = visualObj.AddComponent<SpriteRenderer>();
    }

    public void ResetItem()
    {
        item = null;
        spriteRenderer.sprite = null;
    }
}
