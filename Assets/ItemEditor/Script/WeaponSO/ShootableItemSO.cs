using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootableItemSO : WeaponSO
{
    public float spreadPower = 0.2f;
    public float shootDelay = 0.2f;
    public Projectile bulletPref;
    public EntitySO projectileSO;
}
