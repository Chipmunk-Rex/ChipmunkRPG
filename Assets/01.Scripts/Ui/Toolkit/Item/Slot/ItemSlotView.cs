using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSlotView : VisualElement
{
    public ItemSlotItemView slotItem;
    public int index { get; private set; }
    public ItemContainer ItemContainer { get; private set; }
    public ItemSlotView()
    {
        this.AddToClassList("item-slot");
    }
    public void DrawView(Item item, int index, ItemContainer container)
    {
        this.Clear();
        if (slotItem != null)
            slotItem.RemoveFromHierarchy();

        this.index = index;
        this.ItemContainer = container;

        slotItem = new ItemSlotItemView(item, this);
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
