using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chipmunk.Library.PoolEditor;
using UnityEngine;
using UnityEngine.Events;

public class ItemContainer : INDSerializeAble
{
    #region Properties
    public int SlotLength { get => containerSize.x * containerSize.y; }
    public Vector2Int ContainerSize { get => containerSize; }
    #endregion
    public event Action<int> onSlotDataChanged;
    [SerializeField] Vector2Int containerSize = new Vector2Int(8, 2);
    public Item[] Items { get; private set; }
    private void Awake()
    {
        Items = new Item[SlotLength];
    }
    public virtual void Initialize(Item[] items, Vector2Int containerSize)
    {
        this.Items = items;
        this.containerSize = containerSize;
    }
    public bool AddItem(BaseItemSO itemSO)
    {
        return AddItem(itemSO.CreateItem());
    }
    public bool AddItem(Item item)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (IsSameItem(Items[i], item))
            {
                if (Items[i].CanStack(item.StackCount))
                {
                    Items[i].StackCount += item.StackCount;
                    onSlotDataChanged?.Invoke(i);
                    return true;
                }
            }
        }

        int emptySlotIndex = GetEmptySlotIndex();
        if (emptySlotIndex == -1)
            return false;

        SetItem(emptySlotIndex, item);
        return true;
    }
    private bool IsSameItem(Item item1, Item item2)
    {
        if (item1 == null || item2 == null)
            return false;
        return item1.ItemSO == item2.ItemSO;
    }
    private int GetEmptySlotIndex()
    {
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] == null)
                return i;
        }
        return -1;
    }

    public void SetItem(int slotNum, Item item)
    {
        try
        {
            Items[slotNum] = item;
            if (item != null)
                item.Owner = this;
            onSlotDataChanged?.Invoke(slotNum);
        }
        catch(System.IndexOutOfRangeException e)
        {
            Debug.LogError("ItemContainer.SetItem() : IndexOutOfRangeException" + slotNum + " " + item);
            throw e;
        }
    }
    public Item GetItem(int slotNum)
    {
        try
        {

            return Items[slotNum];
        }
        catch
        {
            return null;
        }
    }
    public int GetItemIndex(Item item)
    {
        if (item == null)
            return -1;

        for (int i = 0; i < Items.Length; i++)
        {
            if (IsSameItem(Items[i], item))
                return i;
        }
        return -1;
    }
    internal void RemoveItem(Item item)
    {
        RemoveItem(item, item.StackCount);
    }
    public void RemoveItem(Item item, int removeCount)
    {
        while (removeCount > 0)
        {
            int itemIndex = GetItemIndex(item);
            if (itemIndex == -1)
            {
                Debug.LogError("ItemContainer.RemoveItem() : Item not found");
                return;
            }
            if (Items[itemIndex].StackCount > removeCount)
            {
                Items[itemIndex].StackCount -= removeCount;
                onSlotDataChanged?.Invoke(itemIndex);
                removeCount = 0;
            }
            else
            {
                removeCount -= Items[itemIndex].StackCount;
                SetItem(itemIndex, null);
            }
        }
    }
    public void DropItem(int slotNum, Vector2 position, World world = null)
    {
        Item item = GetItem(slotNum);
        if (item == null)
            return;
        if (world == null)
            world = World.Instance;
        SetItem(slotNum, null);

        ItemEntity itemEntity = new ItemEntity();

        itemEntity.Initialize(item);

        EntitySpawnEvent @event = new EntitySpawnEvent(world, itemEntity, position);
        world.worldEvents.Execute(EnumWorldEvent.EntitySpawn, @event);
    }

    public virtual NDSData Serialize()
    {
        NDSData data = new NDSData();
        List<NDSData> itemNDSDatas = new List<NDSData>();
        for (int i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null)
            {
                NDSData itemNDSData = Items[i].Serialize();
                itemNDSData.AddData("slotNum", i);
                itemNDSDatas.Add(itemNDSData);
            }
        }

        data.AddData("containerSize", containerSize);
        data.AddData("items", itemNDSDatas);
        return data;
    }

    public virtual void Deserialize(NDSData data)
    {
        for (int i = 0; i < Items.Length; i++)
            SetItem(i, null);
        containerSize = data.GetData<Vector2Int>("containerSize");
        List<NDSData> itemNDSDatas = data.GetData<List<NDSData>>("items");
        foreach (NDSData itemNDSData in itemNDSDatas)
        {
            int slotNum = itemNDSData.GetData<int>("slotNum");
            BaseItemSO itemSO = SOAddressSO.Instance.GetSOByID<BaseItemSO>(uint.Parse(itemNDSData.GetDataString("ItemSO")));
            Item item = itemSO.CreateItem();
            item.Deserialize(itemNDSData);
            SetItem(slotNum, item);
        }
    }
    public void SwapItem(int slotNum1, int slotNum2)
    {
        Item item1 = GetItem(slotNum1);
        Item item2 = GetItem(slotNum2);
        SetItem(slotNum1, item2);
        SetItem(slotNum2, item1);
    }
}
