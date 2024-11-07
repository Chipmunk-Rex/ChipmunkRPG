using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
using UnityEngine;
public class ItemEntity : EntityCompo, IPoolAble
{
    public string PoolName => "ItemEntity";
    public GameObject ObjectPref => gameObject;
    public Item item { get; private set; }
    private float spawnedTime;
    static private float notCollectableTime = 0.8f;

    public void Initialize(Item item)
    {
        this.item = item;
        gameObject.name = $"Item ({item.ItemSO.itemName})";
        SpriteRendererCompo.sprite = item.ItemSO.itemSprite;
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        spawnedTime = Time.time;
    }
    public void OnPoped()
    {

    }

    public void OnPushed()
    {
        item = null;
        SpriteRendererCompo.sprite = null;
    }

    internal void Collect(ItemContainer target, float magneticPower)
    {
        if (Time.time - spawnedTime < notCollectableTime)
        {
            return;
        }
        StartCoroutine(CollectRoutine(target, magneticPower));
    }

    private IEnumerator CollectRoutine(ItemContainer target, float magneticPower)
    {
        float distance = Vector2.Distance(transform.position, target.transform.position);
        float duration = distance / magneticPower;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(transform.position, target.transform.position, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        target.AddItem(item);
        PoolManager.Instance.Push(this);
    }
}
