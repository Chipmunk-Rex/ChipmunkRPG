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
        Debug.Log("Shoot");
        Vector2 shootDir = dir;

        Projectile projectile = wandSO.projectileSO.CreateEntity() as Projectile;
        projectile.SpawnEntity(world);
        projectile.transform.position = transfrom.position;

        projectile.Shoot(dir);
    }
}
