using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Chipmunk.Library;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "EntityData", menuName = "SO/EntityData")]
public abstract class EntitySO : ScriptableObject
{
    public string entityName;
    public string description;
    public Sprite defaultSprite;
    public GameObject shadowPrefab;
    public RuntimeAnimatorController animatorController;
    public int maxHealth = 1;
    public int damage = 1;
    public float speed = 5;
    public float detectRange = 2.5f;
    public float attackRange = 1;
    public bool canCollisions = true;
    public Type test;
    public SerializableDictionary<EnumMeterType, MeterData> meterDatas = new(){
        {EnumMeterType.Health, new MeterData()},
        {EnumMeterType.Hunger, new MeterData()},
        {EnumMeterType.Mentality, new MeterData()},
        {EnumMeterType.Temperature, new MeterData()},
        {EnumMeterType.Thirsty, new MeterData()},
    };
    protected abstract Entity CreateEntityInstance();
    public virtual Entity CreateEntity()
    {
        return CreateEntityInstance().Initialize(this);
    }
}
