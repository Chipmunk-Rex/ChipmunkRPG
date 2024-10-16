using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseProjectile
{
    [SerializeField] Rigidbody2D rigidCompo;
    [SerializeField] ParticleSystem particleSystem;
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Reset()
    {
        base.Reset();
        rigidCompo = GetComponent<Rigidbody2D>();
        rigidCompo.gravityScale = 0;

        particleSystem = transform.GetComponentInChildren<ParticleSystem>();
    }
    public void Initialize(Sprite sprite, Vector2 position)
    {
        spriteRendererCompo.sprite = sprite;
        transform.position = position;
    }
    public override void Shoot(Vector2 dir)
    {
        rigidCompo.velocity = dir * stats.moveSpeed;

        float rotation = Mathf.Atan2(dir.x, dir.y);
        particleSystem.startRotation = rotation - 90;
        particleSystem.transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
