using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
// using Newton

public class Test : MonoBehaviour
{
    [ContextMenu("Test")]
    void Awake()
    {
        string json = null;
        Test1 test1 = new Test2();
        (test1 as Test2).value = "ming";
        json = JsonConvert.SerializeObject(test1, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });
        Debug.Log(json);

        Test2 deserialized = null;
        deserialized = (JsonConvert.DeserializeObject<Test1>(json, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto
        }) as Test2);
        Debug.Log(deserialized.value);
    }
}
