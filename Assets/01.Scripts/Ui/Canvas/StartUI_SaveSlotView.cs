using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class StartUI_SaveSlotView : MonoBehaviour
{

    [SerializeField] private Image[] plantsImg = new Image[3];

    [SerializeField] private Animator animator; 
    public string[] slotPathList;
    int selectIndex;
    int SelectIndex
    {
        get => selectIndex;
        set
        {
            if (isSlotAnimPlaying)
                return;
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
    public bool isSlotAnimPlaying;
    public void GetSlotList()
    {
        slotPathList = Directory.GetDirectories($"{Application.persistentDataPath}/Resources/SaveData");
    }
    public void NextSlot()
    {
        SelectIndex++;
        animator.SetTrigger("NextSlot");
    }
    public void BeforeSlot()
    {
        SelectIndex--;
        animator.SetTrigger("BeforeSlot");
    }
    public void DrawView()
    {
        // plantsImg[0].sprite = 
    }
}
