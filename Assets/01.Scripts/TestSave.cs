using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSave : MonoBehaviour
{
    uint id = 0;
    Text text;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            ScriptableObject so = SOAddressSO.Instance.GetSOByID(id);
            Debug.Log(so.name);
            text = GetComponent<Text>();
            text.text = so.name + " " + id;
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            id++;
            if(id >= SOAddressSO.Instance.assetPaths.Count)
            {
                id = 0;
            }
        }
    }
}
