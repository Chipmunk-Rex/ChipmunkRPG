using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
using DG.Tweening;
using UnityEngine;
public class ItemEntity : Entity
{
    public string PoolName => "ItemEntity";
    public GameObject ObjectPref => gameObject;
    public Item item { get; private set; }
    private float spawnedTime;
    static private float notCollectableTime = 0.8f;

    public void Initialize(Item item)
    {
        this.item = item;
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        spawnedTime = Time.time;

        gameObject.name = $"Item ({item.ItemSO.itemName})";
        Visual.sprite = item.ItemSO.itemSprite;

        Visual.transform.DOLocalMoveY(0.5f, 2.0f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    public override void OnPushed()
    {
        base.OnPushed();
        item = null;
        Visual.sprite = null;
        Visual.transform.DOKill();
        Visual.transform.localPosition = Vector2.zero;
    }

    internal void Collect(Inventory target, float magneticPower)
    {
        if (Time.time - spawnedTime < notCollectableTime)
        {
            return;
        }
        StartCoroutine(CollectRoutine(target, magneticPower));
    }

    private IEnumerator CollectRoutine(Inventory target, float magneticPower)
    {
        float distance = Vector2.Distance(transform.position, target.Owner.transform.position);
        float duration = distance / magneticPower;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(transform.position, target.Owner.transform.position, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        target.AddItem(item);
        PoolManager.Instance.Push(entityCompo);
    }

    public override void Die()
    {
        throw new NotImplementedException();
    }
}
