using System;
using System.Collections;
using System.Collections.Generic;
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
    public bool AddItem(Item item)
    {
        for (int i = 0; i < Items.Length; i++)
        {
            Debug.Log("ming");
            if (Items[i] == null)
            {
                Items[i] = item;
                onSlotDataChanged?.Invoke(i);
                return true;
            }
            else if (Items[i] is StackableItem)
            {
                if (item is StackableItem)
                {
                    StackableItem slotItem = (Items[i] as StackableItem);
                    slotItem.itemCount += (item as StackableItem).itemCount;
                    return true;
                }
            }
        }
        return false;
    }
    public void SetItem(int slotNum, Item item)
    {
        Items[slotNum] = item;
        onSlotDataChanged?.Invoke(slotNum);
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
    public void DropItem(int slotNum, World world, Vector2 position)
    {
        Item item = GetItem(slotNum);
        if (item == null)
            return;
        SetItem(slotNum, null);

        ItemEntity itemEntity = PoolManager.Instance.Pop("ItemEntity").GetComponent<ItemEntity>();
        itemEntity.Initialize(item);

        EntitySpawnEvent @event = new EntitySpawnEvent(world, itemEntity, position);
        world.worldEvents.Execute(EnumWorldEvent.EntitySpawn, @event);
    }

    public NDSData Serialize()
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

    public void Deserialize(NDSData data)
    {
        for (int i = 0; i < Items.Length; i++)
            SetItem(i, null);
        containerSize = data.GetData<Vector2Int>("containerSize");
        List<NDSData> itemNDSDatas = data.GetData<List<NDSData>>("items");
        foreach (NDSData itemNDSData in itemNDSDatas)
        {
            int slotNum = itemNDSData.GetData<int>("slotNum");
            ItemSO itemSO = SOAddressSO.Instance.GetSOByID<ItemSO>(uint.Parse(itemNDSData.GetDataString("ItemSO")));
            Item item = itemSO.CreateItem();
            item.Deserialize(itemNDSData);
            SetItem(slotNum, item);
        }
    }
}
