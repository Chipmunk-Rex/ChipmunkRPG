using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseProjectile
{
    public override void Awake()
    {
        base.Awake();
    }
    protected override void OnShoot(Vector2 dir)
    {
        RigidCompo.velocity = dir;
    }
    public void Initialize(Sprite sprite, Vector2 position)
    {
        Visual.sprite = sprite;
        transform.position = position;
    }

    public override void Die()
    {
        
    }
}
