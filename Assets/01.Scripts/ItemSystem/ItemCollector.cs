using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] public EntityCompo targetCompo;
    [SerializeField] public float collectRange;
    [SerializeField] private float magneticPower = 2;
    [SerializeField] private ContactFilter2D contactFilter2D;
    [SerializeField] int maxDetectCount = 3;
    [SerializeField] private Collider2D[] collects;
    private IInventoryOwner inventoryOwner = null;
    public Inventory inventory => inventoryOwner.Inventory;
    void Awake()
    {
        targetCompo = transform.parent.GetComponent<EntityCompo>();
        Debug.LogWarning("ItemCollector: " + targetCompo);
        Debug.Log(targetCompo.Entity is IInventoryOwner);

        if (targetCompo.Entity is IInventoryOwner)
            Initialize();
        else
            targetCompo.OnSpawnEvent.AddListener(Initialize);

        collects = new Collider2D[maxDetectCount];
    }

    private void Initialize()
    {
        inventoryOwner = targetCompo.Entity as IInventoryOwner;
        targetCompo.OnSpawnEvent.RemoveListener(Initialize);
    }

    void FixedUpdate()
    {
        Debug.Log(inventoryOwner);
        if (inventoryOwner == null)
            return;
        int count = Physics2D.OverlapCircle(transform.position, collectRange, contactFilter2D, collects);
        for (int i = 0; i < count; i++)
        {
            if (collects[i].TryGetComponent(out EntityCompo entityCompo))
            {
                if (entityCompo.Entity is ItemEntity item)
                    item.Collect(inventory, magneticPower);
            }
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, collectRange);
    }
#endif
}