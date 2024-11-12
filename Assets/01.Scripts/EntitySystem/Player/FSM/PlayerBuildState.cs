using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;

public class PlayerBuildState : FSMState<EnumEntityState, Player>
{
    public PlayerBuildState(IFSMEntity<EnumEntityState, Player> entity, string animName) : base(entity, animName)
    {
    }

    public override void UpdateState()
    {
    }
}
