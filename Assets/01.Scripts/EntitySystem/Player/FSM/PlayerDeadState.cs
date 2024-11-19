using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;

public class PlayerDeadState : FSMState<EnumEntityState, Player>
{
    public PlayerDeadState(IFSMEntity<EnumEntityState, Player> entity, string animName) : base(entity, animName)
    {
    }

    public override void UpdateState()
    {
        throw new NotImplementedException();
    }
    public override bool CanChangeTo(EnumEntityState targetState)
    {
        return false;
    }
}