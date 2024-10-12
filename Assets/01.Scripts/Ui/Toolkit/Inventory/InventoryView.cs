using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemContainerView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ItemContainerView, VisualElement.UxmlTraits> { }
    public ItemContainer ItemContainer { get; private set; }
    private ItemSlotView[] itemSlots;
    public void DrawView(ItemContainer itemContainer)
    {
        if (ItemContainer != null)
            ItemContainer.onSlotDataChanged.RemoveListener(OnSlotDataChanged);

        this.ItemContainer = itemContainer;
        itemContainer.onSlotDataChanged.AddListener(OnSlotDataChanged);
        InitializeItemSlot(itemContainer);
    }

    private void InitializeItemSlot(ItemContainer itemContainer)
    {
        int SlotLength = itemContainer.SlotLength;

        itemSlots = new ItemSlotView[SlotLength - 1];

        for (int i = 0; i < SlotLength - 1; i++)
        {
            Item item = itemContainer.GetItem(i);

            ItemSlotView itemSlot = new ItemSlotView();
            itemSlots[i] = itemSlot;
            itemSlot.DrawView(item);
            this.Add(itemSlot);
        }
    }

    private void OnSlotDataChanged(int slotNum)
    {

    }
}
