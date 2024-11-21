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
            int prevIndex = selectedIndex;
            selectedIndex = Mathf.Clamp(value, 0, hotbarSize - 1);
            if (prevIndex != selectedIndex)
            {
                onSelectedIndexChange?.Invoke(selectedIndex);
            }
        }
    }


    protected Item targetUseItem => inventory.GetItem(SelectedIndex);
    protected IInteractableItem beforeFrameItem;
    protected float lastInteractedTime;
    protected float interactStartedTime;

    private IEnumerator useItemCoroutine;
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
        PlayerInputReader.Instance.onWheel += OnWheel;
    }
    private IEnumerator UseItemCoroutine()
    {
        while (targetUseItem != null)
        {
            IInteractableItem targetItem = targetUseItem as IInteractableItem;
            if ((Time.time - lastInteractedTime) > targetItem.interactableItemSO.InteractCool)
                ItemInteract(targetItem as IInteractableItem);
            yield return null;
        }
        if (beforeFrameItem != null)
        {
            EndInteract(beforeFrameItem, true);
        }
    }
    public void UseItem(IInteractableItem item)
    {
        if (useItemCoroutine != null)
        {
            Owner.StopCoroutine(useItemCoroutine);
            if (beforeFrameItem != null)
            {
                EndInteract(beforeFrameItem, true);
            }
        }
        int index = inventory.GetItemIndex(item as Item);
        if(index != -1)
            SelectedIndex = index;
        if (targetUseItem == null || item == null) return;

        useItemCoroutine = UseItemCoroutine();
        Owner.StartCoroutine(useItemCoroutine);
    }


    private void ItemInteract(IInteractableItem targetItem)
    {
        if (targetItem != beforeFrameItem)
        {
            StartInteract(targetItem);
        }

        if ((Time.time - interactStartedTime) < targetItem.interactableItemSO.InteractDuration)
        {
            DuringInteract(targetItem);
        }
        else
        {
            EndInteract(targetItem);
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

    public void OnWheel(float value)
    {
        SelectedIndex -= (int)value / 120;
    }
    public Item GetSelectedItem()
    {
        return inventory.GetItem(SelectedIndex);
    }
}
