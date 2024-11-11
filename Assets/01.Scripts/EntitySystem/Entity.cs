using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
using UnityEngine;

public abstract class Entity : INDSerializeAble
{
    public EventMediatorContainer<EnumEntityEvent, EntityMoveEvent> EntityEvents { get; private set; } = new();
    public EntityCompo entityCompo { get; set; }
    public Transform transform => entityCompo.transform;
    public GameObject gameObject => entityCompo.gameObject;
    public SpriteRenderer SpriteRendererCompo => entityCompo.SpriteRendererCompo;
    public Animator AnimatorCompo => entityCompo.AnimatorCompo;
    public Rigidbody2D RigidCompo => entityCompo.RigidCompo;

    public EntitySO EntitySO { get; private set; }
    public bool IsSpawned => currentWorld != null;
    public World currentWorld;

    public Action onSpawn;
    public string entityName;
    public Vector2 lookDir = Vector2.down;
    public Entity Initialize<T>(T entitySO) where T : EntitySO
    {
        EntitySO = entitySO;
        entityName = entitySO.entityName;
        return this;
    }
    public void SpawnEntity(World world, EntityCompo entityCompo)
    {
        this.entityCompo = entityCompo;
        currentWorld = world;
        if (world != null)
        {
            EntitySpawnEvent @event = new EntitySpawnEvent(world, this);
            world.worldEvents.Execute(EnumWorldEvent.EntitySpawn, @event);
        }
    }
    public void SpawnEntity(World world)
    {
        Debug.Log(world);
        currentWorld = world;
        if (world != null)
        {
            EntitySpawnEvent @event = new EntitySpawnEvent(world, this);
            world.worldEvents.Execute(EnumWorldEvent.EntitySpawn, @event);
        }
    }
    public virtual void OnSpawn()
    {
        SpriteRendererCompo.sprite = EntitySO.defaultSprite;
        AnimatorCompo.runtimeAnimatorController = EntitySO.animatorController;

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

    public void StartCoroutine(IEnumerator enumerator)
    {
        entityCompo.StartCoroutine(enumerator);
    }
    public void StopCoroutine(IEnumerator enumerator)
    {
        entityCompo.StopCoroutine(enumerator);
    }
    public virtual void Deserialize(NDSData data) { }

    public virtual void OnPushed() { }

}
