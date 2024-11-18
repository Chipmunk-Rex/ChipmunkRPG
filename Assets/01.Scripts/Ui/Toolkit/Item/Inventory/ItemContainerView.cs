using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemContainerView : VisualElement
{
    public new class UxmlFactory : UxmlFactory<ItemContainerView, VisualElement.UxmlTraits> { }
    public ItemContainer ItemContainer { get; private set; }
    public ItemSlotView[] itemSlots { get; private set; }
    public ItemContainerView()
    {
        this.style.flexWrap = Wrap.Wrap;
    }
    public void DrawView(ItemContainer itemContainer)
    {
        if (ItemContainer != null)
            ItemContainer.onSlotDataChanged -= OnSlotDataChanged;

        this.ItemContainer = itemContainer;
        Debug.Log(itemContainer);
        itemContainer.onSlotDataChanged += OnSlotDataChanged;
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
                ItemSlotView itemSlot = CreateSlotView(index);
                rowContainer.Add(itemSlot);
                itemSlot.DrawView(item, index, ItemContainer);
            }
        }
    }


    private ItemSlotView CreateSlotView(int i)
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

    private void OnSlotDataChanged(int slotNum)
    {
        Item item = ItemContainer.GetItem(slotNum);
        itemSlots[slotNum].DrawView(item, slotNum, ItemContainer);
    }
}
