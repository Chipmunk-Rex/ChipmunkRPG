using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chipmunk.Library;
using UnityEngine;

public class Animal_IdleState : FSMState<EnumEntityState, Animal>
{
    public Animal_IdleState(IFSMEntity<EnumEntityState, Animal> entity, string animName) : base(entity, animName)
    {
    }

    public override void UpdateState()
    {

        Entity[] entities = entity.DetectEntities();
        Entity player = entities.Where(e => e is Player).FirstOrDefault();
        if (player != null)
        {
            entity.FSMStateMachine.ChangeState(EnumEntityState.Move);
        }
    }
}
