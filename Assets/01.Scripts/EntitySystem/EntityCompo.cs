using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Chipmunk.Library;
using Chipmunk.Library.PoolEditor;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[DisallowMultipleComponent]
public class EntityCompo : MonoBehaviour, IPoolAble, IDamageable
{
    public World currentWorld;
    public EntityStats stats;
    #region CompoRegion
    [field: SerializeField] public Animator AnimatorCompo { get; protected set; }
    [field: SerializeField] public SpriteRenderer SpriteRendererCompo { get; protected set; }
    [field: SerializeField] public Rigidbody2D RigidCompo { get; protected set; }
    [field: SerializeField] public BehaviorTree BehaviorTreeCompo { get; protected set; }
    [field: SerializeField] public Inventory InventoryCompo { get; protected set; }
    #endregion
    [field: SerializeField] public EntitySO EntitySO { get; protected set; }
    public Entity entity { get; protected set; }

    public string PoolName => "Entity";

    public GameObject ObjectPref => gameObject;

    public void TakeDamage(int damage)
    {
        throw new NotImplementedException();
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
    protected virtual void OnEnable()
    {
        if(EntitySO != null)
        {
            entity = EntitySO.CreateEntity();
            entity.SpawnEntity(this);
        }
        entity?.OnEnable();
    }
    protected virtual void OnDisable() => entity?.OnDisable();
    protected virtual void Awake() => entity?.Awake();

    protected virtual void Update() => entity?.Update();
    protected virtual void FixedUpdate() => entity?.FixedUpdate();
    public void OnPoped() => entity?.OnPoped();
    public void OnPushed() => entity?.OnPushed();
    public virtual void OnSpawn()
    {

    }
}
