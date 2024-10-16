using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHotbar : MonoBehaviour
{
    #region Getter
    public ItemContainer ItemContainer => itemContainer;
    public event Action<InventoryHotbar> OnHotbarChenged;
    public event Action<int> onSelectedIndexChange;
    #endregion

    [SerializeField] protected ItemContainer itemContainer;
    [SerializeField] protected Entity owner;
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
            OnHotbarChenged?.Invoke(this);
        }
    }
    private int hotbarSize = 5;

    protected virtual void Awake()
    {
        OnHotbarChenged?.Invoke(this);
        PlayerInputReader.Instance.onWheel += ChangeSelectedIndex;
    }
    void Reset()
    {
        itemContainer = transform.parent.GetComponent<Inventory>();
    }
    void Update()
    {
        if (targetUseItem != null)
        {
            if ((Time.time - lastInteractedTime) > targetUseItem.interactableItemSO.InteractCool)
                UseItem();
        }
        else
        {
            if (beforeFrameItem != null)
            {
                beforeFrameItem.OnEndInteract(owner);
                lastInteractedTime = Time.time;
                beforeFrameItem = null;
            }
        }
    }

    private void UseItem()
    {
        if (targetUseItem != beforeFrameItem)
        {
            targetUseItem.OnBeforeInteract(owner);
            interactStartedTime = Time.time;
            beforeFrameItem = targetUseItem;
        }

        if ((Time.time - interactStartedTime) < targetUseItem.interactableItemSO.InteractDuration)
            targetUseItem.OnInteract(owner);
        else
        {
            targetUseItem.OnEndInteract(owner);
            lastInteractedTime = Time.time;
            beforeFrameItem = null;
        }
    }

    public void ChangeSelectedIndex(float value)
    {
        Debug.Log(value);
        SelectedIndex += (int)value / 120;
        onSelectedIndexChange?.Invoke(SelectedIndex);
    }
    public Item GetSelectedItem()
    {
        return itemContainer.GetItem(SelectedIndex);
    }
}
