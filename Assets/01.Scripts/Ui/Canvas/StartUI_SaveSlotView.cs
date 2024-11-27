using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartUI_SaveSlotView : MonoBehaviour
{

    [SerializeField] private Image[] plantsImg = new Image[3];

    [SerializeField] private Animator animator;
    public string[] slotPathList;

    [Header("Slot Data View")]
    public TMP_Text slotName;
    public TMP_Text slotDate;
    public TMP_Text slotDesc;

    int selectIndex;
    int SelectIndex
    {
        get => selectIndex;
        set
        {
            if (isSlotChangeAnimPlaying)
                return;

            if (selectIndex < value)
                animator.SetTrigger("NextSlot");
            else if (selectIndex > value)
                animator.SetTrigger("BeforeSlot");

            selectIndex = ClampIndex(value);
            if (selectIndex != value)
                DrawView();
        }
    }
    public int ClampIndex(int index)
    {
        if (index < 0)
            return slotPathList.Length - 1;
        else if (index >= slotPathList.Length)
            return 0;
        return index;
    }
    private bool isSlotChangeAnimPlaying;
    public void AnimStart() => isSlotChangeAnimPlaying = true;
    public void AnimEnd() => isSlotChangeAnimPlaying = false;

    public void GetSlotList()
    {
        slotPathList = Directory.GetDirectories($"{Application.dataPath}/Resources/SaveData");
    }
    public void NextSlot()
    {
        SelectIndex++;
    }
    public void BeforeSlot()
    {
        SelectIndex--;
    }
    public void CreateSlot()
    {
        
    }
    public void DrawView()
    {
        string slotNameStr = "damaged Slot";
        string slotLastDateStr = "";
        try
        {
            string json = File.ReadAllText($"{slotPathList[SelectIndex]}/SlotData.json");
            Debug.Log(json);
            NDSData slotNDSData = JsonConvert.DeserializeObject<NDSData>(json);
            slotNameStr = slotPathList[SelectIndex].Split('/')[slotPathList[SelectIndex].Split('/').Length - 1];
            slotLastDateStr = slotNDSData.GetDataString("LastSaveDate");
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        }

        slotName.text = slotNameStr;
        slotDate.text = slotLastDateStr;

        // plantsImg[0].sprite = 
    }
}
