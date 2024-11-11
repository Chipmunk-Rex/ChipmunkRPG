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
            beforeFrameItem.OnEndInteract(Owner);
            lastInteractedTime = Time.time;
            beforeFrameItem = null;
        }
    }
    public void UseItem(IInteractableItem item)
    {
        Owner.StopCoroutine(UseItemCoroutine());
        targetUseItem = item;
        if (targetUseItem == null) return;
        Debug.Log("UseItem");
        Owner.StartCoroutine(UseItemCoroutine());
    }


    private void ItemInteract()
    {
        if (targetUseItem != beforeFrameItem)
        {
            targetUseItem.OnBeforeInteract(Owner);
            interactStartedTime = Time.time;
            beforeFrameItem = targetUseItem;
        }

        if ((Time.time - interactStartedTime) < targetUseItem.interactableItemSO.InteractDuration)
            targetUseItem.OnInteract(Owner);
        else
        {
            targetUseItem.OnEndInteract(Owner);
            lastInteractedTime = Time.time;
            beforeFrameItem = null;
        }
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
