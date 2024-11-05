using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

[Serializable]
public struct NDSData
{
    [field: SerializeField] public SerializableDictionary<string, string> Data { get; set; }
    public NDSData(SerializableDictionary<string, string> data = null)
    {
        Data = data ?? new SerializableDictionary<string, string>();
    }
    public void AddData(string key, object value)
    {
        string json = JsonConvert.SerializeObject(value, new JsonSerializerSettings
        { TypeNameHandling = TypeNameHandling.All }
        );
        if (Data.ContainsKey(key))
        {
            Data[key] = json;
        }
        else
        {
            Data.Add(key, json);
        }
    }
    public void RemoveData(string key)
    {
        if (Data.ContainsKey(key))
        {
            Data.Remove(key);
        }
    }
    public string GetData(string key)
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
            object value = JsonConvert.DeserializeObject(Data[key], new JsonSerializerSettings
            { TypeNameHandling = TypeNameHandling.All }
            );
            return (T)value;
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
            value = JsonConvert.DeserializeObject<T>(Data[key], new JsonSerializerSettings
            { TypeNameHandling = TypeNameHandling.All }
            );
            return true;
        }
        value = default;
        return false;
    }
    public void Clear()
    {
        Data.Clear();
    }
}