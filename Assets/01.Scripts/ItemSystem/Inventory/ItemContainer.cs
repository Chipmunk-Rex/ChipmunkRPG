using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] Vector2Int containerSize = new Vector2Int(8, 2);
    [SerializeField] Item[] items;
    private void Awake()
    {
        items = new Item[containerSize.x * containerSize.y];
    }
    public bool AddItem(Item item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            Item tempItem = items[i];

            if (item is StackableItem)
            {
                if (tempItem != null && tempItem is StackableItem)
                {
                    StackableItem stackableTemp = (tempItem as StackableItem);
                    stackableTemp.itemCount = stackableTemp.itemCount + (item as StackableItem).itemCount;
                    return true;
                }
            }
            if (tempItem == null)
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
    public void DropItem(int slotNum)
    {
        Item item = GetItem(slotNum);
        
        ItemEntity itemEntity = PoolManager.Instance.Pop("ItemEntity").GetComponent<ItemEntity>();
        itemEntity.Initialize(item);
    }
}
