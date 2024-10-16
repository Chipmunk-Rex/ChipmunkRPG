using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
public abstract class Entity : MonoBehaviour
{
    public World currentWorld;
    public EntityStats stats;
    #region CompoRegion
    [field: SerializeField] public Health healthCompo { get; protected set; }
    [field: SerializeField] public Animator animatorCompo { get; protected set; }
    [field: SerializeField] public SpriteRenderer spriteRendererCompo { get; protected set; }
    [field: SerializeField] public BaseEntityMovement movementCompo { get; protected set; }
    #endregion
    [field: SerializeField] public EntitySO entitySO { get; protected set; }
    public Vector2 lookDir = Vector2.down;
    protected virtual void Awake()
    {
        healthCompo = GetComponent<Health>();
        InitializeStats();

        if (currentWorld == null)
        {
            SpawnEntity(ChipmunkLibrary.GetComponentWithParent<World>(transform), entitySO);
        }
    }

    public void SpawnEntity(World world, EntitySO entitySO)
    {
        this.entitySO = entitySO;
        animatorCompo.runtimeAnimatorController = entitySO.animatorController;
        if (entitySO != null)
        {
            InitializeStats();
        }
        SpawnEntity(world);
    }
    public void SpawnEntity(World world)
    {
        currentWorld = world;
        if (world != null)
        {
            EntitySpawnEvent @event = new EntitySpawnEvent(world, this);
            world.worldEvents.Execute(EnumWorldEvent.EntitySpawn, @event);
        }
    }
    public virtual void OnSpawn()
    {

    }

    private void InitializeStats()
    {
        if (entitySO == null) return;
        stats.moveSpeed = entitySO.moveSpeed;
        stats.maxHP = entitySO.maxHP;
        stats.attackDamage = entitySO.attackDamage;
        stats.attackSpeed = entitySO.attackSpeed;
    }

    protected virtual void Reset()
    {
        GameObject visualObj = transform.Find("Visual")?.gameObject;
        if (visualObj == null)
        {
            visualObj = new GameObject("Visual");
            spriteRendererCompo = visualObj.AddComponent<SpriteRenderer>();
            animatorCompo = visualObj.AddComponent<Animator>();
        }
        else
        {
            spriteRendererCompo = visualObj.GetComponent<SpriteRenderer>();
            animatorCompo = visualObj.GetComponent<Animator>();
        }
        visualObj.transform.SetParent(this.transform);
        spriteRendererCompo.sortingLayerName = "Entity";
    }
}
