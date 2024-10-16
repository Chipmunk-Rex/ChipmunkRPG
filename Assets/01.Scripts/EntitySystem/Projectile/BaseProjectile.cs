using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : Entity
{
    [SerializeField] protected float lifeTime = 8f;
    private float spawnedTime;
    public abstract void Shoot(Vector2 dir);
    public override void OnSpawn()
    {
        base.OnSpawn();
        spawnedTime = Time.time;
    }
    protected virtual void FixedUpdate()
    {
        if ((Time.time - spawnedTime) > lifeTime)
        {
            Debug.Log(Time.time - spawnedTime);
            Destroy(this.gameObject); // 나중에 풀 가능한 아이템이면 다시 넣는 코드 작성필요s
        }
    }
}
