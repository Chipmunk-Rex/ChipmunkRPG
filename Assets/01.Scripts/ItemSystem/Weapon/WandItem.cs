using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
using UnityEngine;

public class WandItem : WeaponItem
{
    private WandSO wandSO;
    public WandItem(BaseItemSO itemSO) : base(itemSO)
    {
        wandSO = itemSO as WandSO;
    }

    public override IInteractableItemSO interactableItemSO => wandSO;

    public override void OnBeforeInteract(Entity target)
    {
    }

    public override void OnEndInteract(Entity target)
    {
    }
    float lastShootTime;
    public override void OnInteract(Entity target)
    {
        if (Time.time - lastShootTime > wandSO.shootDelay)
        {
            lastShootTime = Time.time;
            Shoot(target.currentWorld, target.lookDir, target.transform.position);
        }
    }
    public void Shoot(World world, Vector2 dir, Vector2 pos)
    {
        Vector2 shootDir = dir;

        Projectile projectilePref = wandSO.bulletPref;
        Projectile projectile = null;
        if (wandSO.bulletPref.TryGetComponent(out IPoolAble poolable))
        {
            projectile = PoolManager.Instance.Pop(poolable.PoolName).GetComponent<Projectile>();
        }
        else
        {
            projectile = GameObject.Instantiate(projectilePref);
        }
        projectile.SpawnEntity(world);
        projectile.Initialize(null, pos);
        projectile.Shoot(dir);
    }
}
