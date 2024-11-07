using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
using UnityEngine;

public abstract class Entity : INDSerializeAble
{
    public EventMediatorContainer<EnumEntityEvent, EntityMoveEvent> EntityEvents { get; private set; } = new();
    protected EntityCompo entityCompo { get; private set; }
    protected Transform transform => entityCompo.transform;
    public GameObject gameObject => entityCompo.gameObject;
    public SpriteRenderer SpriteRendererCompo => entityCompo.SpriteRendererCompo;
    public Rigidbody2D RigidCompo => entityCompo.RigidCompo;

    public EntitySO EntitySO { get; private set; }

    public bool IsSpawned => isSpawned;
    public bool isSpawned;

    public string entityName;
    public Vector2 lookDir = Vector2.down;
    public void SpawnEntity(EntityCompo entityCompo)
    {
        this.entityCompo = entityCompo;
        // entityCompo.entity = this;
        isSpawned = true;
        entityCompo.OnSpawn();
    }
    public EntityCompo SpawnEntity()
    {
        entityCompo = PoolManager.Instance.Pop("Entity").GetComponent<EntityCompo>();
        SpriteRendererCompo.sprite = EntitySO.defaultSprite;
        return entityCompo;
    }
    private void Spawn()
    {

    }
    public virtual void Initialize(EntitySO entitySO)
    {
        this.EntitySO = entitySO;
        entityName = entitySO.entityName;
    }
    public virtual void Awake() { }
    public virtual void OnEnable() { }
    public virtual void OnDisable() { }

    public virtual void Update() { }
    public virtual void FixedUpdate() { }
    public T GetComponent<T>() where T : Component
    {
        return gameObject.GetComponent<T>();
    }
    public virtual NDSData Serialize()
    {
        NDSData entityNDSData = new NDSData();
        entityNDSData.AddData("Position", new JsonVector2(transform.position));
        entityNDSData.AddData("EntitySO", SOAddressSO.Instance.GetIDBySO(EntitySO));
        entityNDSData.AddData("LookDir", new JsonVector2(lookDir));
        return entityNDSData;
    }
    public virtual void Deserialize(NDSData data) { }

    public virtual void OnPoped() { }

    public virtual void OnPushed() { }
}
