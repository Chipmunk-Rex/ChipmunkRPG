using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    [SerializeField] private BaseItemSO itemSO;
    [SerializeField] private Inventory inventory;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Item item = itemSO.CreateItem();
            inventory.AddItem(item);
        }
    }
}
