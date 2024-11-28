using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
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

    public Image fadeOutImage;

    [SerializeField] int selectIndex;
    int SelectIndex
    {
        get => selectIndex;
        set
        {
            int beforeIndex = selectIndex;
            if (isSlotChangeAnimPlaying)
                return;

            if (selectIndex < value)
                animator.SetTrigger("NextSlot");
            else if (selectIndex > value)
                animator.SetTrigger("BeforeSlot");

            selectIndex = ClampIndex(value);
            if (selectIndex != beforeIndex)
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
        string slotDescStr = "slot path : " + slotPathList[SelectIndex];
        Debug.Log(SelectIndex);
        try
        {
            string json = File.ReadAllText($"{slotPathList[SelectIndex]}/SlotData.json");
            SlotData slotData = JsonConvert.DeserializeObject<SlotData>(json);
            slotNameStr = slotPathList[SelectIndex].Split('/')[slotPathList[SelectIndex].Split('/').Length - 1];
            slotLastDateStr = slotData.lastOpenDate;
            slotDescStr = slotData.desc.ToString();
        }
        catch
        {
        }

        slotName.text = slotNameStr;
        slotDate.text = slotLastDateStr;
        slotDesc.text = slotDescStr;

        // plantsImg[0].sprite = 
    }
    public UnityEvent onSelected;
    public void Select()
    {
        SlotManager.Instance.currentSlotPath = slotPathList[SelectIndex];
        onSelected?.Invoke();
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
