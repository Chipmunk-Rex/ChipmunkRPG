using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnumWorldEvent
{
    #region Build 0 ~ 100
    BuildingCreate = 001, BuildingSet = 002, BuildingRemove = 003,
    #endregion

    #region Entity 100 ~ 200
    EntitySpawn = 101, EntityDeath = 102, EntityDamaged
    #endregion
}
