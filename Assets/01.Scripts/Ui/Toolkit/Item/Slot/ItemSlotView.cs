using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSlotView : VisualElement
{
    public ItemSlotItemView slotItem;
    public ItemSlotView()
    {
    }
    public void DrawView(Item item)
    {
        this.Clear();
        
        slotItem = new ItemSlotItemView(item);
        this.Add(slotItem);

        DrawVisual(item);
        DrawCount(item);
    }
    private void DrawVisual(Item item)
    {
        if (item == null)
        {
            slotItem.SetSprite(null);
        }
        else
        {
            slotItem.SetSprite(item.ItemSO.itemSprite);
        }

    }

    private void DrawCount(Item item)
    {
        int count = 0;
        if (item != null)
        {
            if (item is StackableItem)
                count = (item as StackableItem).itemCount;
            else
                count = 1;
        }

        slotItem.SetItemCount(count);
    }
}
