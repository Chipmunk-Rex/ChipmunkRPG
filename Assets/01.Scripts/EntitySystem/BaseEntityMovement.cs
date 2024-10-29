using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class BaseEntityMovement : MonoBehaviour
{
    protected Vector2 moveDir;
    protected Entity entity;
    public Rigidbody2D rigidBodyCompo { get; private set; }
    protected virtual void Awake()
    {
        rigidBodyCompo = GetComponent<Rigidbody2D>();
        rigidBodyCompo.gravityScale = 0;

        entity = GetComponent<Entity>();
    }
    public abstract void Move(Vector2 moveDir);

    internal void Initailize(float moveSpeed)
    {
        throw new NotImplementedException();
    }
}
