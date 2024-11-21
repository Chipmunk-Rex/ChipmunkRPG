using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemVisualable
{
    public SpriteRenderer ItemSpriteCompo { get; }
    public Animator ItemAnimatorCompo { get; }
    public Vector2 LookDir { get; }

    void OnVisual(Item item)
    {
        Debug.Log("OnVisual");
        if(LookDir.y > 0)
            ItemSpriteCompo.sortingOrder = Mathf.RoundToInt(-1);
        else
            ItemSpriteCompo.sortingOrder = Mathf.RoundToInt(1);

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