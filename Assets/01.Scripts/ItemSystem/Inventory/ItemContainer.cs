using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] Vector2Int containerSize = new Vector2Int(8, 2);
    Item[] items;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            DropItem(0, world);
        }
    }
    public bool AddItem(Item item)
    {
        if (item is StackableItem)
        {
            for (int i = 0; i < items.Length; i++)
            {
                Item tempItem = items[i];
                if (tempItem != null && tempItem is StackableItem)
                {
                    StackableItem stackableTemp = (tempItem as StackableItem);
                    stackableTemp.itemCount = stackableTemp.itemCount + (item as StackableItem).itemCount;
                    return true;
                }
            }
        }

        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log("ming");
            if (items[i] == null)
            {
                items[i] = item;
                return true;
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
    }
}
