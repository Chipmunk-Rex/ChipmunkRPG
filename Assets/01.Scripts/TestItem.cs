using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    [SerializeField] private BaseItemSO itemSO;
    [SerializeField] private EntityCompo playerCompo;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Item item = itemSO.CreateItem();
            (playerCompo.Entity as Player).Inventory.AddItem(item);
        }
        if(Input.GetKeyDown(KeyCode.G))
        {
            (playerCompo.Entity as Player).Inventory.DropItem(0, playerCompo.transform.position, World.Instance);
        }
    }
}
