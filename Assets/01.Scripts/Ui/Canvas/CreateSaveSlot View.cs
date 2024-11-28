using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreateSaveSlotView : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TMP_InputField slotName;
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] TMP_InputField seed;
    public void Open()
    {
        this.gameObject.SetActive(true);
        animator.SetBool("Open", true);
    }
    public void Close()
    {
        animator.SetBool("Open", false);
    }
    public void CreateSaveSlot()
    {
        string slotName = this.slotName.text;
        string slotPath = $"{Application.dataPath}/Resources/SaveData/{slotName}";
        if (!System.IO.Directory.Exists(slotPath))
        {
            System.IO.Directory.CreateDirectory(slotPath);

            SlotData slotData = new SlotData();
            try
            {
                slotData.seed = int.Parse(seed.text);
            }
            catch
            {
                slotData.seed = Random.Range(int.MinValue, int.MaxValue);
            }
            slotData.lastOpenDate = System.DateTime.Now.ToString();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(slotData);
            System.IO.File.WriteAllText($"{slotPath}/slotData.json", json);
        }
        else
        {
            Debug.Log("Save Slot Already Exists");
        }
        SlotManager.Instance.currentSlotPath = slotPath;
    }
}
