using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct NDSData
{
    public SerializableDictionary<string, object> Data { get; set; }
    public NDSData(SerializableDictionary<string, object> data = null)
    {
        Data = data ?? new SerializableDictionary<string, object>();
    }
    public void AddData(string key, object value)
    {
        if (Data.ContainsKey(key))
        {
            Data[key] = value;
        }
        else
        {
            Data.Add(key, value);
        }
    }
    public void RemoveData(string key)
    {
        if (Data.ContainsKey(key))
        {
            Data.Remove(key);
        }
    }
    public object GetData(string key)
    {
        if (Data.ContainsKey(key))
        {
            return Data[key];
        }
        return null;
    }
    public T GetData<T>(string key)
    {
        if (Data.ContainsKey(key))
        {
            return (T)Data[key];
        }
        return default;
    }
    public bool TryGetData(string key, out object value)
    {
        if (Data.ContainsKey(key))
        {
            value = Data[key];
            return true;
        }
        value = null;
        return false;
    }
    public bool TryGetData<T>(string key, out T value)
    {
        if (Data.ContainsKey(key))
        {
            value = (T)Data[key];
            return true;
        }
        value = default;
        return false;
    }

}
