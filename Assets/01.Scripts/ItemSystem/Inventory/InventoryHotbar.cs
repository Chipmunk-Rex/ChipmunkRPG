using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHotbar : MonoBehaviour
{
    [SerializeField] ItemContainer itemContainer;
    private int selectedIndex = 0;
    public int SelectedIndex
    {
        get => selectedIndex;
        set
        {
            selectedIndex = Mathf.Clamp(value, 0, hotbarSize - 1);
        }
    }
    int hotbarSize = 5;
    private void Awake()
    {
        PlayerInputReader.Instance.onWheel += ChangeSelectedIndex;
    }

    private void ChangeSelectedIndex(float value)
    {
        Debug.Log(value);
        SelectedIndex += (int)value / 120;
    }
}
