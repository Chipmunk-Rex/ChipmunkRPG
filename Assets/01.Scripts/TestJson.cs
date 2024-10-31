using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJson : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TestJsonDataPer data = new TestJsonDataPer();
            string json = JsonUtility.ToJson(data);
            Debug.Log(json);
            data = new TestJsonData();
            json = JsonUtility.ToJson(data);
            Debug.Log(json);
        }
    }
}
