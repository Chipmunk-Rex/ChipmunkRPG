using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseProjectile
{
    ParticleSystem particleSystem;
    public ParticleSystem Particle => particleSystem;
    public override void Awake()
    {
        base.Awake();
    }
    protected override void OnShoot(Vector2 dir)
    {
        particleSystem = transform.GetComponentInChildren<ParticleSystem>();
        float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotation);

        RigidCompo.velocity = dir;
    }
    public void Initialize(Sprite sprite, Vector2 position)
    {
        SpriteRendererCompo.sprite = sprite;
        transform.position = position;
    }
}
