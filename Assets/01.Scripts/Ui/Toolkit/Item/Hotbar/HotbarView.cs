using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HotbarView : VisualElement
{

    public new class UxmlFactory : UxmlFactory<HotbarView, UxmlTraits> { }
    private ItemSlotView[] itemSlots;
    InventoryHotbar hotbar;
    ItemContainer itemContainer => hotbar.Inventory;

    public void InitializeView(InventoryHotbar hotbar)
    {
        this.hotbar = hotbar;
        itemContainer.onSlotDataChanged += OnSlotDataChanged;
        hotbar.onSelectedIndexChange += OnSelectedSlotChanged;
        hotbar.OnHotbarSizeChanged += DrawView;
        DrawView(hotbar);

    }

    ItemSlotView selectedSlot;
    private void OnSelectedSlotChanged(int index)
    {
        if (selectedSlot != null)
            selectedSlot.RemoveFromClassList("SelectedSlot");

        selectedSlot = itemSlots[index];
        selectedSlot.AddToClassList("SelectedSlot");
    }

    private void DrawView(InventoryHotbar hotbar)
    {
        this.Clear();
        int hotbarSize = hotbar.HotbarSize;

        itemSlots = new ItemSlotView[hotbarSize];
        for (int index = 0; index < hotbarSize; index++)
        {
            Item item = itemContainer.GetItem(index);
            ItemSlotView itemSlot = CreateSlotView(index, item);
            this.Add(itemSlot);
            itemSlot.DrawView(item, index, itemContainer);
        }
    }

    private ItemSlotView CreateSlotView(int i, Item item)
    {
        ItemSlotView itemSlot = new ItemSlotView();
        try
        {
            itemSlots[i] = itemSlot;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            Debug.Log(i);
        }
        return itemSlot;
    }

    private void OnSlotDataChanged(int value)
    {
        if (value < hotbar.HotbarSize)
        {
            Item item = itemContainer.GetItem(value);
            itemSlots[value].DrawView(item, value, itemContainer);
        }
    }
}
