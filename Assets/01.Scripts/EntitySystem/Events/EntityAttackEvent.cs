using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttackEvent : EntityEvent
{
    public EntityAttackEvent(Entity sender, EntityCompo targetEntity) : base(targetEntity)
    {
    }

    public override EnumEventResult ExcuteEvent()
    {
        throw new System.NotImplementedException();
    }
}
