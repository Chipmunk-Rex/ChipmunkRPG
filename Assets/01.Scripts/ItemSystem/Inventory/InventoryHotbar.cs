using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InventoryHotbar
{
    #region Getter
    public ItemContainer Inventory => inventory;
    public event Action<InventoryHotbar> OnHotbarSizeChanged;
    public event Action<int> onSelectedIndexChange;
    #endregion

    protected Inventory inventory;
    protected Entity Owner { get; private set; }
    private protected int selectedIndex = 0;
    public int SelectedIndex
    {
        get => selectedIndex;
        set
        {
            selectedIndex = Mathf.Clamp(value, 0, hotbarSize - 1);
        }
    }


    protected IInteractableItem targetUseItem;
    protected IInteractableItem beforeFrameItem;
    protected float lastInteractedTime;
    protected float interactStartedTime;
    public int HotbarSize
    {
        get => hotbarSize;
        set
        {
            hotbarSize = value;
            OnHotbarSizeChanged?.Invoke(this);
        }
    }
    private int hotbarSize = 5;
    public InventoryHotbar(Entity owner, Inventory inventory, int hotbarSize)
    {
        this.Owner = owner;
        this.inventory = inventory;
        this.hotbarSize = hotbarSize;

        OnHotbarSizeChanged?.Invoke(this);
        PlayerInputReader.Instance.onWheel += ChangeSelectedIndex;

        inventory.onSlotDataChanged += (slotNum) =>
        {
            if (slotNum == SelectedIndex)
                onSelectedIndexChange?.Invoke(SelectedIndex);
        };
    }
    private IEnumerator UseItemCoroutine()
    {
        while (targetUseItem != null)
        {
            if ((Time.time - lastInteractedTime) > targetUseItem.interactableItemSO.InteractCool)
                ItemInteract();
            yield return null;
        }
        if (beforeFrameItem != null)
        {
            EndInteract(beforeFrameItem, true);
        }
    }
    public void UseItem(IInteractableItem item)
    {
        Owner.StopCoroutine(UseItemCoroutine());
        targetUseItem = item;
        if (targetUseItem == null) return;
        
        Owner.StartCoroutine(UseItemCoroutine());
    }


    private void ItemInteract()
    {
        if (targetUseItem != beforeFrameItem)
        {
            StartInteract(targetUseItem);
        }

        if ((Time.time - interactStartedTime) < targetUseItem.interactableItemSO.InteractDuration)
        {
            DuringInteract(targetUseItem);
        }
        else
        {
            EndInteract(targetUseItem);
        }
    }
    private void StartInteract(IInteractableItem targetItem)
    {
        targetItem.OnBeforeInteract(Owner);

        if (Owner is IItemInteractHandler itemInteractHandler)
            itemInteractHandler.OnBeforeInteract(targetItem as Item);
        if (Owner is IItemVisualable itemVisualable)
            itemVisualable.OnVisual(targetItem as Item);
        interactStartedTime = Time.time;
        beforeFrameItem = targetItem;
    }

    private void DuringInteract(IInteractableItem targetItem)
    {
        targetItem.OnInteract(Owner);
        if (Owner is IItemInteractHandler itemInteractHandler)
            itemInteractHandler.OnInteract(targetItem as Item);
    }
    private void EndInteract(IInteractableItem targetItem, bool isCanceled = false)
    {
        targetItem.OnEndInteract(Owner, isCanceled);
        if (Owner is IItemInteractHandler itemInteractHandler)
            itemInteractHandler.OnEndInteract(targetItem as Item, isCanceled);
        if (Owner is IItemVisualable itemVisualable)
            itemVisualable.OnVisual(null);
        lastInteractedTime = Time.time;
        beforeFrameItem = null;
    }

    public void ChangeSelectedIndex(float value)
    {
        SelectedIndex -= (int)value / 120;
        onSelectedIndexChange?.Invoke(SelectedIndex);
    }
    public Item GetSelectedItem()
    {
        return inventory.GetItem(SelectedIndex);
    }
}
