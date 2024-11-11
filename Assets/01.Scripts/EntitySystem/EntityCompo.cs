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
    #region CompoRegion
    [field: SerializeField] public EntitySO EntitySO { get; protected set; }
    [field: SerializeField] public Animator AnimatorCompo { get; protected set; }
    [field: SerializeField] public SpriteRenderer SpriteRendererCompo { get; protected set; }
    [field: SerializeField] public Rigidbody2D RigidCompo { get; protected set; }
    [field: SerializeField] public Inventory InventoryCompo { get; protected set; }
    #endregion
    public Entity Entity { get; set; }

    public string PoolName => "Entity";

    public GameObject ObjectPref => gameObject;

    public void TakeDamage(int damage)
    {
        throw new NotImplementedException();
    }
    protected virtual void OnEnable()
    {
        if (EntitySO != null)
        {
            Entity = EntitySO.CreateEntity();
            Entity.SpawnEntity(ChipmunkLibrary.GetComponentWithParent<World>(transform), this);
        }
        Entity?.OnEnable();
    }
    protected virtual void OnDisable() => Entity?.OnDisable();
    protected virtual void Awake() => Entity?.Awake();

    protected virtual void Update() => Entity?.Update();
    protected virtual void FixedUpdate() => Entity?.FixedUpdate();
    public void OnPoped() => Entity?.OnPoped();
    public void OnPushed()
    {
        Entity?.OnPushed();
        Entity = null;
    }
}
