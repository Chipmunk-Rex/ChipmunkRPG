using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotData
//  : INDSerializeAble
{
    public int seed = int.MinValue;
    public string lastOpenDate = "";
    public string desc = "";
    // public void Deserialize(NDSData data)
    // {
    //     seed = data.GetData<int>("seed");
    //     lastOpenDate = data.GetData<string>("lastOpenDate");
    // }

    // public NDSData Serialize()
    // {
    //     NDSData data = new NDSData();
    //     data.AddData("seed", seed);
    //     data.AddData("lastOpenDate", lastOpenDate);

    //     return data;
    // }
}
