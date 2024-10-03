using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EntityMovement : MonoBehaviour
{
    Vector2 moveDir;
    private Entity entity;
    public Rigidbody2D rigidBodyCompo { get; private set; }
    private void Awake()
    {
        rigidBodyCompo = GetComponent<Rigidbody2D>();
        rigidBodyCompo.gravityScale = 0;

        entity = GetComponent<Entity>();
    }
    public void Move(Vector2 moveDir)
    {
        this.moveDir = moveDir;
        rigidBodyCompo.velocity = moveDir * entity.stats.moveSpeed;
    }
}
