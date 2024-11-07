using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Chipmunk.Library;
using Chipmunk.Library.PoolEditor;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[DisallowMultipleComponent]
public class Entity : MonoBehaviour, IPoolAble, IDamageable
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

    public string PoolName => "Entity";

    public GameObject ObjectPref => gameObject;

    public Vector2 lookDir = Vector2.down;
    public EventMediatorContainer<EnumEntityEvent, EntityMoveEvent> entityEvents;
    public int hp;
    private int moveSpeed;
    protected virtual void Awake()
    {
        if (currentWorld == null)
        {
            if (EntitySO != null)
            {
                Initialize(EntitySO);
                SpawnEntity(ChipmunkLibrary.GetComponentWithParent<World>(transform));
            }
        }
    }
    public void Initialize(EntityJsonData entityJsonData)
    {
        Initialize(entityJsonData.entitySO);
        stats = entityJsonData.stats;
        transform.position = entityJsonData.position;
        lookDir = entityJsonData.lookDir;
        hp = entityJsonData.hp;
    }
    public void Initialize(EntitySO entitySO)
    {
        this.EntitySO = entitySO;
        hp = entitySO.maxHP;
        BehaviorTreeCompo.enabled = entitySO.externalBehaviorTree != null;
        BehaviorTreeCompo.ExternalBehavior = entitySO.externalBehaviorTree;
    }
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
    public virtual void OnSpawn()
    {

    }

    protected virtual void Reset()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Entity");
        GameObject visualObj = transform.Find("Visual")?.gameObject;
        if (visualObj == null)
        {
            visualObj = new GameObject("Visual");
            SpriteRendererCompo = visualObj.AddComponent<SpriteRenderer>();
            AnimatorCompo = visualObj.AddComponent<Animator>();
        }
        else
        {
            SpriteRendererCompo = visualObj.GetComponent<SpriteRenderer>();
            AnimatorCompo = visualObj.GetComponent<Animator>();
        }
        visualObj.transform.SetParent(this.transform);
        SpriteRendererCompo.sortingLayerName = "Entity";
    }

    public void InitializeItem()
    {
    }

    public void ResetItem()
    {
    }
}
