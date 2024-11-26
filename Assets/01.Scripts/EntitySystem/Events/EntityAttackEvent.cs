using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityAttackEvent : EntityEvent
{
    public Entity[] targetEntities;
    public int damage;
    public EntityAttackEvent(Entity sender, Entity[] targetEntities, int damage) : base(sender)
    {
        this.targetEntities = targetEntities;
        this.damage = damage;
    }
    public EntityAttackEvent(Entity sender, Entity targetEntity, int damage) : base(sender)
    {
        this.targetEntities = new Entity[] { targetEntity };
        this.damage = damage;
    }

    public override EnumEventResult ExcuteEvent()
    {
        foreach (var target in targetEntities)
        {
            if (target.meters.ContainsKey(EnumMeterType.Health))
                target.meters[EnumMeterType.Health].Value -= 10;
        }
        return EnumEventResult.Successed;
    }
}
