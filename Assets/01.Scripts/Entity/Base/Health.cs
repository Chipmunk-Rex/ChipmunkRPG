using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    private int hp;
    public int HP
    {
        get => hp;
        set
        {
            if (value - hp < 0)
                onDamaged?.Invoke(value);

            hp = value;

            if (hp <= 0)
                onDeath?.Invoke();
        }
    }
    public UnityEvent<int> onDamaged;
    public UnityEvent onDeath;
}
