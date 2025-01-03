using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NDSTest : MonoBehaviour, INDSerializeAble
{
    [SerializeField] int testInt;
    [SerializeField] NDSData data = new NDSData();

    public void Deserialize(NDSData data)
    {
        // testInt = int.Parse(data.GetData("testInt"));
    }

    public NDSData Serialize()
    {
        data.Clear();
        return data;
    }

    [ContextMenu("Test")]
    public void Test()
    {
        data.Clear();
        Serialize();
        Debug.Log(data.GetData("testInt"));
        Deserialize(data);
        
        Debug.Log(data.Serialize());
        Debug.Log(NDSData.Deserialize(data.Serialize()));
    }
}
