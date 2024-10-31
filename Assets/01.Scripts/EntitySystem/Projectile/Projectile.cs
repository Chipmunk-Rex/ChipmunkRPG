using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseProjectile
{
    #region Getter
    public ParticleSystem Particle => particleSystem;
    #endregion
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
        SpriteRendererCompo.sprite = sprite;
        transform.position = position;
    }
    public override void Shoot(Vector2 dir)
    {

        float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        
        rigidCompo.velocity = dir * stats.moveSpeed;
    }
}
