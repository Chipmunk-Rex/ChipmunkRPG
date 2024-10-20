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
            Shoot(target.currentWorld, target.lookDir, target.transform);
        }
    }
    public void Shoot(World world, Vector2 dir, Transform transfrom)
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
        projectile.SpawnEntity(world, wandSO.projectileSO);
        projectile.Initialize(null, transfrom.position);
        projectile.transform.SetParent(transfrom);  

        var main = projectile.Particle.main;
        var zR = main.startRotationZ;
        zR.constant = Mathf.Atan2(dir.y, dir.x) + 90 * Mathf.Deg2Rad;
        main.startRotationZ = zR;

        projectile.Shoot(dir);
    }
}
