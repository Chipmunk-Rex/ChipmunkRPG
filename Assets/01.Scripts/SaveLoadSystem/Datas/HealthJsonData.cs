using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HealthJsonData
{
    public int currentHealth;
    public int maxHealth;
    public HealthJsonData(Health health)
    {
        currentHealth = health.HP;
        maxHealth = health.MaxHP;
    }
}
