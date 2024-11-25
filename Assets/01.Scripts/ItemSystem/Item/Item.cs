using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : INDSerializeAble
{
    int stackCount = 1;
    public ItemContainer Owner { get; set; }
    public int StackCount
    {
        get => stackCount;
        set
        {
            stackCount = value;
            if (stackCount <= 0)
            {
                stackCount = 0;
                Owner.RemoveItem(this);
            }
            if (stackCount > ItemSO.maxStackCount)
            {
                stackCount = ItemSO.maxStackCount;
                Debug.LogError("StackCount is over maxStackCount");
            }
        }
    }
    public bool CanStack(int count)
    {
        return stackCount + count <= ItemSO.maxStackCount;
    }
    public BaseItemSO ItemSO { get; protected set; }
    public Item(BaseItemSO itemSO)
    {
        ItemSO = itemSO;
    }

    public NDSData Serialize()
    {
        NDSData data = new NDSData();
        data.AddData("ItemSO", SOAddressSO.Instance.GetIDBySO(ItemSO));
        return data;
    }

    public void Deserialize(NDSData data)
    {
        ItemSO = SOAddressSO.Instance.GetSOByID<BaseItemSO>(uint.Parse(data.GetDataString("ItemSO")));
    }
}
