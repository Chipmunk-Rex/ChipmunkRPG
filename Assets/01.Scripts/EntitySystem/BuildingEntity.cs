using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildingEntity : Entity
{
    override public void OnSpawn()
    {
        base.OnSpawn();
    }
    public override void Die()
    {
        World.Instance.RemoveBuilding(parentBuilding.pos);
        parentBuilding.buildingEntity = null;
        parentBuilding = null;
    }
}
