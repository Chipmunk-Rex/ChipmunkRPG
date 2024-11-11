using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Chipmunk.Library;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "EntityData", menuName = "SO/EntityData")]
public abstract class EntitySO : ScriptableObject
{
    public string entityName;
    public Sprite defaultSprite;
    public GameObject shadowPrefab;
    public RuntimeAnimatorController animatorController;
    public int maxHealth = 1;
    public int damage = 1;
    public float speed = 5;
    public float attackRange = 1;
    public float attackRate = 1;
    public float attackDuration = 0.5f;
    public float attackCooldown = 0.5f;
    public float attackDelay = 0.5f;
    public float attackKnockback = 1;
    protected abstract Entity CreateEntityInstance();
    public virtual Entity CreateEntity()
    {
        return CreateEntityInstance().Initialize(this);
    }
}
