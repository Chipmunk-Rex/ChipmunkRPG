using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : Entity
{
    [SerializeField] protected float lifeTime = 8f;
    private float spawnedTime;
    public virtual void Shoot(Vector2 dir)
    {
        spawnedTime = Time.time;
    }
    protected abstract void OnShoot(Vector2 dir);
    public override void FixedUpdate()
    {
        if ((Time.time - spawnedTime) > lifeTime)
        {
            GameObject.Destroy(this.gameObject); // 나중에 풀링 가능한 아이템이면 다시 넣는 코드 작성필요s
        }
    }
}
