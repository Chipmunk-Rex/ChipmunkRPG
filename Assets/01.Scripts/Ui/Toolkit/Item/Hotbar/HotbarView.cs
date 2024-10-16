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
    ItemContainer itemContainer => hotbar.ItemContainer;

    public void InitializeView(InventoryHotbar hotbar)
    {
        this.hotbar = hotbar;
        itemContainer.onSlotDataChanged.AddListener(OnSlotDataChanged);
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
        itemSlot.DrawView(item);
        return itemSlot;
    }

    private void OnSlotDataChanged(int value)
    {
        Debug.Log("OnSlotDataChanged");
        if (value < hotbar.HotbarSize)
        {
            Item item = itemContainer.GetItem(value);
            itemSlots[value].DrawView(item);
        }
    }
}
