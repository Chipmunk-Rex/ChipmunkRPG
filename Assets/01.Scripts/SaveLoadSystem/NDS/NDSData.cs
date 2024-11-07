using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

[Serializable]
public struct NDSData
{
    [field: SerializeField] public SerializableDictionary<string, string> data;
    public SerializableDictionary<string, string> Data
    {
        get
        {
            if(data == null)
            {
                data = new SerializableDictionary<string, string>();
            }
            return data;
        }
    }
    public NDSData(SerializableDictionary<string, string> data = null)
    {
        this.data = data ?? new SerializableDictionary<string, string>();
    }
    public void AddData(string key, INDSerializeAble value)
    {
        string json = JsonConvert.SerializeObject(value.Serialize()
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
    public void AddData(string key, object value)
    {
        if (Data == null)
            Debug.Log("Data is null");
        string json = ToString(value);
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
    public string GetDataString(string key)
    {
        if (Data.ContainsKey(key))
        {
            return Data[key];
        }
        return null;
    }
    public NDSData GetData(string key)
    {
        if (Data.ContainsKey(key))
        {
            NDSData value = JsonConvert.DeserializeObject<NDSData>(Data[key]
            );
            return value;
        }
        return null;
    }
    public T GetData<T>(string key)
    {
        if (Data.ContainsKey(key))
        {
            T value = JsonConvert.DeserializeObject<T>(Data[key]
            );
            return value;
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
            value = JsonConvert.DeserializeObject<T>(Data[key]
            );
            return true;
        }
        value = default;
        return false;
    }
    public string Serialize()
    {
        return JsonConvert.SerializeObject(Data, new JsonSerializerSettings
        { TypeNameHandling = TypeNameHandling.All }
        );
    }
    public static NDSData Deserialize(string json)
    {
        return new NDSData(JsonConvert.DeserializeObject<SerializableDictionary<string, string>>(json, new JsonSerializerSettings
        { TypeNameHandling = TypeNameHandling.All }
        ));
    }

    public static implicit operator NDSData(SerializableDictionary<string, string> data)
    {
        return new NDSData(data);
    }
    public void Clear()
    {
        Data.Clear();
    }
    public static string ToString(object obj)
    {
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        { TypeNameHandling = TypeNameHandling.All }
        );
    }
    public static T ToObject<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
        { TypeNameHandling = TypeNameHandling.All }
        );
    }
}