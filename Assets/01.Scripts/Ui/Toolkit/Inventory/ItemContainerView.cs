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
        this.Clear();

        Vector2Int size = itemContainer.ContainerSize;
        int SlotLength = itemContainer.SlotLength;

        itemSlots = new ItemSlotView[SlotLength];
        for (int y = 0; y < size.y; y++)
        {
            VisualElement rowContainer = new VisualElement();
            rowContainer.name = "RowContainer";
            rowContainer.AddToClassList("rowContainer");
            rowContainer.style.flexDirection = FlexDirection.Row;
            this.Add(rowContainer);
            for (int x = 0; x < size.x; x++)
            {
                int index = y * size.x + x;
                Item item = itemContainer.GetItem(index);
                ItemSlotView itemSlot = CreateSlotView(index, item);
                rowContainer.Add(itemSlot);
            }
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

    private void OnSlotDataChanged(int slotNum)
    {
        Item item = ItemContainer.GetItem(slotNum);
        itemSlots[slotNum].DrawView(item);
    }
}
