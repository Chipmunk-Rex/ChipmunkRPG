using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSlotView : VisualElement
{
    public ItemSlotItemView slotItem;
    public Label countLbl;
    public ItemSlotView()
    {
        slotItem = new ItemSlotItemView();
        this.Add(slotItem);
        countLbl = new Label("0");
        countLbl.visible = false;
        this.Add(countLbl);
    }
    public void DrawView(Item item)
    {
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
        if (item == null)
        {
            countLbl.visible = false;
        }

        if (item is StackableItem)
            countLbl.text = (item as StackableItem).itemCount.ToString();
        else
            countLbl.text = "1";
    }
}
