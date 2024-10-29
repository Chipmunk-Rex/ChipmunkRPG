using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int MaxHP => maxHp;
    private int hp;
    private int maxHp;
    public int HP
    {
        get => hp;
        set
        {
            if (value - hp < 0)
                OnDamaged(value);

            hp = value;

            if (hp <= 0)
                onDeath?.Invoke();
        }
    }
    public void Initailize(int maxHp)
    {
        this.maxHp = maxHp;
        hp = maxHp;
    }

    public UnityEvent<int> onDamaged;
    public UnityEvent onDeath;
    private void OnDamaged(int value)
    {
        onDamaged?.Invoke(value);
    }
}
