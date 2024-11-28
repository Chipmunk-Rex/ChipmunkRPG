using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chipmunk.Library;
using UnityEngine;

public class Animal_RunAwayState : FSMState<EnumEntityState, Animal>
{
    public Animal_RunAwayState(IFSMEntity<EnumEntityState, Animal> entity, string animName) : base(entity, animName)
    {
    }

    public override void UpdateState()
    {
        Entity[] entities = entity.DetectEntities();
        Entity player = entities.Where(e => e is Player).FirstOrDefault();
        if (player != null)
        {
            EntityMoveEvent entityEvent = new EntityMoveEvent(entity, entity.transform.position - player.transform.position);
            entity.EntityEvents.Execute(EnumEntityEvent.Move, entityEvent);
        }
    }
}
