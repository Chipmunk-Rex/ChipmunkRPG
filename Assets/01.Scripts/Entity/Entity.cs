using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Health))]
public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected Health health;
    [field: SerializeField] public EntityDataSO entitySO;
    public Vector2 lookDir = Vector2.down;
    public World currentWorld;
    protected virtual void Awake()
    {
        health = ChipmunkLibrary.GetComponentWhenNull(this, ref health);
    }
}
