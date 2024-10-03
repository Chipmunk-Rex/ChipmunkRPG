using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;

public class PlayerIdleState : FSMState<EnumPlayerState, Player>
{
    public PlayerIdleState(IFSMEntity<EnumPlayerState, Player> entity, string animName) : base(entity, animName)
    {
    }

    public override void UpdateState()
    {
        
    }
}
