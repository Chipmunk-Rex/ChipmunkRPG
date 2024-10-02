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
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [field: SerializeField] public EntityDataSO entitySO;
    public Vector2 lookDir = Vector2.down;
    public World currentWorld;
    protected virtual void Awake()
    {
        health = ChipmunkLibrary.GetComponentWhenNull(this, ref health);
    }
    private void Reset()
    {
        GameObject visualObj = new GameObject("Visual");
        visualObj.transform.SetParent(this.transform);
        spriteRenderer = visualObj.AddComponent<SpriteRenderer>();
    }
}
