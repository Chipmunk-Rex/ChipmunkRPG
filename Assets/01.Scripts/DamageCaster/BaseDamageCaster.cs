using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDamageCaster : MonoBehaviour
{
    [SerializeField] protected int detectCount = 1;
    [SerializeField] protected ContactFilter2D contactFilter;
    public abstract void CaseDamage(int damage = 1, float knockbackPower = 0f);
}
