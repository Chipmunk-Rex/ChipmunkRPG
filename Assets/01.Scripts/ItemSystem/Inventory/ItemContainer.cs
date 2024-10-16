using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
using UnityEngine;
using UnityEngine.Events;

public class ItemContainer : MonoBehaviour
{
    #region Properties
    public int SlotLength { get => containerSize.x * containerSize.y; }
    public Vector2Int ContainerSize { get => containerSize; }
    #endregion
    public UnityEvent<int> onSlotDataChanged;
    [SerializeField] public World world;
    [SerializeField] ItemContainerType containerType = ItemContainerType.Default;
    [SerializeField] Vector2Int containerSize = new Vector2Int(8, 2);
    [SerializeField] private ItemSO itemSO;
    private Item[] items;
    private void Awake()
    {
        items = new Item[SlotLength];
        Debug.Log(items[0] == null);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Item item = itemSO.CreateItem();
            Debug.Log($"{items[0] == null} {AddItem(item)} {items[0] == null}");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            DropItem(0, world);
        }
    }
    public bool AddItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log("ming");
            if (items[i] == null)
            {
                items[i] = item;
                onSlotDataChanged?.Invoke(i);
                return true;
            }
            else if (items[i] is StackableItem)
            {
                if (item is StackableItem)
                {
                    StackableItem slotItem = (items[i] as StackableItem);
                    slotItem.itemCount += (item as StackableItem).itemCount;
                    return true;
                }
            }
        }
        return false;
    }
    public void SetItem(int slotNum, Item item)
    {
        items[slotNum] = item;
        onSlotDataChanged?.Invoke(slotNum);
    }
    public Item GetItem(int slotNum)
    {
        try
        {

            return items[slotNum];
        }
        catch
        {
            return null;
        }
    }
    public void DropItem(int slotNum, World world)
    {
        Item item = GetItem(slotNum);
        SetItem(slotNum, null);

        ItemEntity itemEntity = PoolManager.Instance.Pop("ItemEntity").GetComponent<ItemEntity>();
        itemEntity.Initialize(item);

        EntitySpawnEvent @event = new EntitySpawnEvent(world, itemEntity, transform.position);
        world.worldEvents.Execute(EnumWorldEvent.EntitySpawn, @event);
    }
}
