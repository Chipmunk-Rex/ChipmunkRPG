using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(BaseStats))]
[RequireComponent(typeof(Health))]
public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected Health health;
    [SerializeField] protected BaseStats stats;
    protected virtual void Awake()
    {
        health = ChipmunkLibrary.GetComponentWhenNull(this, ref health);
        stats = ChipmunkLibrary.GetComponentWhenNull(this, ref stats);


    }
}
