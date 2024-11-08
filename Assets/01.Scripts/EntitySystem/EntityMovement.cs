using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : BaseEntityMovement
{
    public override void Move(Vector2 moveDir)
    {
        this.moveDir = moveDir;
        rigidBodyCompo.velocity = moveDir * entity.EntitySO.speed;
    }
}
