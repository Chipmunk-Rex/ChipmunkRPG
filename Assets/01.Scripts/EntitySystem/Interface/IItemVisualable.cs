using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemVisualable
{
    public SpriteRenderer ItemSpriteCompo { get; }
    public Animator ItemAnimatorCompo { get; }

    void OnVisual(Item item)
    {
        Debug.Log("OnVisual");
        if (item == null)
        {
            ItemSpriteCompo.sprite = null;
            // ItemAnimatorCompo.runtimeAnimatorController = null;
            return;
        }
        ItemSpriteCompo.sprite = item.ItemSO.itemSprite;
        // ItemAnimatorCompo.runtimeAnimatorController = item.ItemSO.ItemA;
    }
}
