using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item : INDSerializeAble
{
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
