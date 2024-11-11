using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] public Inventory target;
    [SerializeField] public float collectRange;
    [SerializeField] private float magneticPower = 2;
    [SerializeField] private ContactFilter2D contactFilter2D;
    [SerializeField] int maxDetectCount = 3;
    [SerializeField] private Collider2D[] collects;
    private void Reset()
    {
        target = transform.parent.GetComponent<Inventory>();

    }
    void Awake()
    {
        collects = new Collider2D[maxDetectCount];
    }
    void FixedUpdate()
    {
        int count = Physics2D.OverlapCircle(transform.position, collectRange, contactFilter2D, collects);
        for(int i = 0; i < count; i++)
        {
            if (collects[i].TryGetComponent(out EntityCompo entityCompo))
            {
                if(entityCompo.Entity is ItemEntity item)
                    item.Collect(target, magneticPower);
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