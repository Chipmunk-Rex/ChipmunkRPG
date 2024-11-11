using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSO : EntitySO
{
    protected override Entity CreateEntityInstance()
    {
        return new Projectile();
    }
}
