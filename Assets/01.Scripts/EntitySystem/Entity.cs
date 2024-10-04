using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(EntityMovement))]
public abstract class Entity : MonoBehaviour
{
    public World currentWorld;
    public EntityStats stats;
    #region CompoRegion
    [field: SerializeField] public Health healthCompo { get; protected set; }
    [field: SerializeField] public Animator animatorCompo { get; protected set; }
    [field: SerializeField] public SpriteRenderer spriteRendererCompo { get; protected set; }
    [field: SerializeField] public EntityMovement movementCompo { get; protected set; }
    #endregion
    [field: SerializeField] public EntitySO entitySO { get; protected set; }
    public Vector2 lookDir = Vector2.down;
    protected virtual void Awake()
    {
        healthCompo = GetComponent<Health>();
        InitializeStats();

        if (currentWorld == null && entitySO != null)
        {
            SpawnEntity();
        }
    }

    public void SpawnEntity(EntitySO entitySO)
    {
        this.entitySO = entitySO;
        InitializeStats();
        SpawnEntity();
    }
    private void SpawnEntity()
    {
        currentWorld = ChipmunkLibrary.GetComponentWithParent<World>(transform);
        if (currentWorld != null)
        {
            EntitySpawnEvent @event = new EntitySpawnEvent(currentWorld, this);
            currentWorld.worldEvents.Execute(EnumWorldEvent.EntitySpawn, @event);
        }
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
        GameObject visualObj = new GameObject("Visual");
        visualObj.transform.SetParent(this.transform);
        spriteRendererCompo = visualObj.AddComponent<SpriteRenderer>();
        spriteRendererCompo.sortingLayerName = "Entity";
        animatorCompo = visualObj.AddComponent<Animator>();

        movementCompo = GetComponent<EntityMovement>();
    }
}
