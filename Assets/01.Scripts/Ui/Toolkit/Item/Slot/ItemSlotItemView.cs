using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemSlotItemView : VisualElement
{
    VisualElement visual;
    public Label countLbl;
    public readonly Item item;
    private ItemSlotView slot;
    public ItemSlotItemView(Item item, ItemSlotView slot)
    {
        this.item = item;
        this.slot = slot;

        this.RegisterCallback<MouseUpEvent>(OnMouseUp);
        this.AddManipulator(new DragAndDropManipulator(this, slot.parent.parent.parent));

        visual = new VisualElement();
        visual.name = "ItemVisual";
        this.Add(visual);

        countLbl = new Label("0");
        countLbl.visible = false;
        countLbl.name = "ItemCount";
        this.Add(countLbl);

    }

    private void OnMouseUp(MouseUpEvent evt)
    {
        Debug.Log("OnMouseUp");
        if (evt.button == 0)
        {
            VisualElement root = GetRootElement(this);
            List<ItemContainerView> containerList = root.Query<ItemContainerView>().ToList();

            ItemContainerView wrappedContainer = null;
            foreach (ItemContainerView itemContainer in containerList)
            {
                if (itemContainer.worldBound.Overlaps(this.worldBound))
                    wrappedContainer = itemContainer;
            }
            Debug.Log(wrappedContainer);
            if (wrappedContainer != null)
            {
                UQueryBuilder<VisualElement> allSlots =
                    root.Query<VisualElement>(className: "item-slot");
                UQueryBuilder<VisualElement> overlappingSlots =
                    allSlots.Where(OverlapsTarget);
                VisualElement closestOverlappingSlot =
                    FindClosestSlot(overlappingSlots);

                ItemSlotView wrappedSlot = null;
                for (int i = 0; i < wrappedContainer.itemSlots.Length; i++)
                {
                    ItemSlotView slot = wrappedContainer.itemSlots[i];
                    if (slot.worldBound.Overlaps(this.worldBound))
                    {
                        wrappedSlot = slot;
                        break;
                    }
                }

                if (wrappedSlot != null)
                {
                    SwapItem(wrappedSlot);
                }
            }
            else // containerView와 wrapped되지 않은 경우
            {
                Debug.Log("It's not wrapped");
                int itemIndex = slot.ItemContainer.GetItemIndex(item);
                slot.ItemContainer.DropItem(itemIndex, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }
    private void SwapItem(ItemSlotView slot)
    {
        ItemContainer container1 = slot.ItemContainer;
        ItemContainer container2 = this.slot.ItemContainer;

        Item item1 = container1.GetItem(slot.index);
        Item item2 = container2.GetItem(this.slot.index);

        container1.SetItem(slot.index, item2);
        container2.SetItem(this.slot.index, item1);
    }
    private VisualElement FindClosestSlot(UQueryBuilder<VisualElement> overlappingSlots)
    {
        VisualElement closestSlot = null;
        List<VisualElement> slotsList = overlappingSlots.ToList();
        float minDistance = float.MaxValue;
        foreach (VisualElement slot in slotsList)
        {
            float distance = Vector2.Distance(this.worldBound.position, slot.worldBound.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestSlot = slot;
            }
        }
        return closestSlot;
    }

    private bool OverlapsTarget(VisualElement element)
    {
        return element.worldBound.Overlaps(this.worldBound);
    }

    public static VisualElement GetRootElement(VisualElement element)
    {
        VisualElement currentElement = element;
        while (currentElement.hierarchy.parent != null)
        {
            currentElement = currentElement.hierarchy.parent;
        }
        return currentElement;
    }

    public void SetSprite(Sprite sprite)
    {
        StyleBackground backgroundStyle = visual.style.backgroundImage;
        Background background = backgroundStyle.value;
        background.sprite = sprite;

        visual.style.backgroundImage = background;
    }
    public void SetItemCount(int count)
    {

        if (count == 0)
        {
            countLbl.visible = false;
        }
        countLbl.text = count.ToString();
    }
}
