using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
using UnityEngine;
using UnityEngine.Events;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] ItemContainerType containerType = ItemContainerType.Default;
    [SerializeField] Vector2Int containerSize = new Vector2Int(8, 2);
    public int SlotLength { get => containerSize.x * containerSize.y; }
    public UnityEvent<int> onSlotDataChanged;
    private Item[] items;
    public Item[] Items { get; private set; }
    private void Awake()
    {
        items = new Item[containerSize.x * containerSize.y];
        Debug.Log(items[0] == null);
    }
    [SerializeField] World world;
    [SerializeField] ItemSO itemSO;
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
    public Item GetItem(int slotNum)
    {
        return items[slotNum];
    }
    public void DropItem(int slotNum, World world)
    {
        Item item = GetItem(slotNum);

        ItemEntity itemEntity = PoolManager.Instance.Pop("ItemEntity").GetComponent<ItemEntity>();
        itemEntity.Initialize(item);

        EntitySpawnEvent @event = new EntitySpawnEvent(world, itemEntity);
        world.worldEvents.Execute(EnumWorldEvent.EntitySpawn, @event);
        onSlotDataChanged?.Invoke(slotNum);
    }
}
