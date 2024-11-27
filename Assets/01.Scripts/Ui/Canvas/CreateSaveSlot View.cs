using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateSaveSlotView : MonoBehaviour
{
    [SerializeField] TMP_Text slotName;
    [SerializeField] Dropdown dropdown;
    public void CreateSaveSlot()
    {
        string slotName = this.slotName.text;
        string slotPath = $"{Application.dataPath}/Resources/SaveData/{slotName}";
        if (!System.IO.Directory.Exists(slotPath))
        {
            System.IO.Directory.CreateDirectory(slotPath);
            Debug.Log("Save Slot Created");
        }
        else
        {
            Debug.Log("Save Slot Already Exists");
        }
    }
}
