using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleDamageCaster : BaseDamageCaster
{
    [SerializeField] public float radius = 1f;
    public override void CaseDamage(int damage = 1, float knockbackPower = 0)
    {
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
#endif
}