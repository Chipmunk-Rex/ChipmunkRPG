using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chipmunk.Library.PoolEditor;
using UnityEngine;

public abstract class Entity : INDSerializeAble
{
    public EventMediatorContainer<EnumEntityEvent, EntityMoveEvent> EntityEvents { get; private set; } = new();
    public EntityCompo entityCompo { get; set; }
    public Transform Transform => transform;
    public Transform transform => entityCompo.transform;
    public GameObject gameObject => entityCompo.gameObject;
    public SpriteRenderer Visual => entityCompo.SpriteRendererCompo;
    public Animator AnimatorCompo => entityCompo.AnimatorCompo;
    public Rigidbody2D RigidCompo => entityCompo.RigidCompo;
    public Collider2D ColliderCompo => entityCompo.ColliderCompo;

    public EntitySO EntitySO { get; private set; }
    public bool IsSpawned => World != null;
    public World World;

    public Action onSpawn;
    public string entityName;
    public Vector2 LookDir => lookDir.Value;
    public NotifyValue<Vector2> lookDir = new(Vector2.down);
    public Dictionary<EnumMeterType, Meter> meters = new();

    public float spawnededTime;

    public Building parentBuilding;
    public virtual Entity Initialize<T>(T entitySO) where T : EntitySO
    {
        EntitySO = entitySO;
        entityName = entitySO.entityName;
        try
        {
            meters.Add(EnumMeterType.Health, new MeterHealth(this, entitySO.meterDatas[EnumMeterType.Health]));
            meters.Add(EnumMeterType.Hunger, new MeterHunger(this, entitySO.meterDatas[EnumMeterType.Hunger]));
            meters.Add(EnumMeterType.Thirsty, new MeterThirsty(this, entitySO.meterDatas[EnumMeterType.Thirsty]));
            meters.Add(EnumMeterType.Mentality, new MeterMentality(this, entitySO.meterDatas[EnumMeterType.Mentality]));
            meters.Add(EnumMeterType.Temperature, new MeterTemperature(this, entitySO.meterDatas[EnumMeterType.Temperature]));
        }
        catch
        {
            if (!meters.ContainsKey(EnumMeterType.Health))
                meters.Add(EnumMeterType.Health, null);
            if (!meters.ContainsKey(EnumMeterType.Hunger))
                meters.Add(EnumMeterType.Hunger, null);
            if (!meters.ContainsKey(EnumMeterType.Thirsty))
                meters.Add(EnumMeterType.Thirsty, null);
            if (!meters.ContainsKey(EnumMeterType.Mentality))
                meters.Add(EnumMeterType.Mentality, null);
            if (!meters.ContainsKey(EnumMeterType.Temperature))
                meters.Add(EnumMeterType.Temperature, null);
        }

        return this;
    }
    public void SpawnEntity(World world, EntityCompo entityCompo)
    {
        this.entityCompo = entityCompo;
        World = world;
        if (world != null)
        {
            EntitySpawnEvent @event = new EntitySpawnEvent(world, this);
            world.worldEvents.Execute(EnumWorldEvent.EntitySpawn, @event);
        }
    }
    public void SpawnEntity(World world = null, Vector2 pos = default)
    {
        if (world == null)
            world = World.Instance;
        World = world;
        if (world != null)
        {
            EntitySpawnEvent @event = new EntitySpawnEvent(world, this, pos);
            world.worldEvents.Execute(EnumWorldEvent.EntitySpawn, @event);
        }
    }
    public Entity[] DetectEntities()
    {
        Entity[] entities = null;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, EntitySO.detectRange);
        if (colliders == null || colliders.Length == 0)
        {
            return null;
        }
        else
        {
            entities = colliders.Select(x =>
            {
                EntityCompo entityCompo = x.GetComponent<EntityCompo>();
                if (entityCompo != null)
                    return entityCompo.Entity;
                return null;
            }).ToArray();
        }
        return entities;
    }
    public virtual void OnSpawn()
    {
        if (EntitySO != null)
        {
            Visual.sprite = EntitySO.defaultSprite;
            AnimatorCompo.runtimeAnimatorController = EntitySO.animatorController;
        }

        try
        {

            onSpawn?.Invoke();
            entityCompo.OnSpawnEvent?.Invoke();
            spawnededTime = Time.time;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        if (EntitySO != null)
            if (!EntitySO.canCollisions)
            {
                ColliderCompo.forceSendLayers = 0;
                ColliderCompo.forceReceiveLayers = 0;
            }
    }
    public virtual void OnPushed()
    {
        transform.name = "Entity";
        if (EntitySO != null)
            if (!EntitySO.canCollisions)
            {
                ColliderCompo.forceSendLayers = int.MaxValue;
                ColliderCompo.forceReceiveLayers = int.MaxValue;
            }

    }
    public virtual void Awake() { }
    public virtual void OnEnable() { }
    public virtual void OnDisable() { }

    public virtual void Update() { }
    public virtual void FixedUpdate() { if (Time.time - spawnededTime > EntitySO.lifeTime && EntitySO.lifeTime != -1) Die(); }
    public T GetComponent<T>() where T : Component
    {
        return gameObject.GetComponent<T>();
    }
    public virtual void OnPlayerInteract(Player player) { }
    public virtual NDSData Serialize()
    {
        NDSData entityNDSData = new NDSData();
        entityNDSData.AddData("Position", new JsonVector2(transform.position));
        entityNDSData.AddData("EntitySO", SOAddressSO.Instance.GetIDBySO(EntitySO));
        entityNDSData.AddData("LookDir", new JsonVector2(lookDir.Value));
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
    Vector2 savedPos;
    public bool hasOwner; // World에서 역직렬화 시 owner가 있는지 확인. owner가 있으면 월드에서 관리하지 않음.

    public virtual void Deserialize(NDSData data)
    {
        savedPos = data.GetData<JsonVector2>("Position");
        if (entityCompo == null)
            onSpawn += SetPosition;
        else
            transform.position = savedPos;

        EntitySO = SOAddressSO.Instance.GetSOByID<EntitySO>(uint.Parse(data.GetDataString("EntitySO")));
        lookDir.Value = data.GetData<JsonVector2>("LookDir");
    }
    private void SetPosition()
    {
        Debug.Log(savedPos + "EntitiyPos====");
        transform.position = savedPos;
        onSpawn -= SetPosition;
    }


    public abstract void Die();
}
