using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;

public class PlayerSwimState : FSMState<EnumEntityState, Player>
{
    public PlayerSwimState(IFSMEntity<EnumEntityState, Player> entity, string animName) : base(entity, animName)
    {
    }
    PlayerInputReader inputReader => PlayerInputReader.Instance;
    public override bool CanChangeTo(EnumEntityState targetState)
    {
        if (targetState == EnumEntityState.Idle || targetState == EnumEntityState.SwimIdle)
            return true;
        return false;
    }
    public override void UpdateState()
    {
        if (inputReader.playerMoveDir.Value == Vector2.zero)
        {
            entity.FSMStateMachine.ChangeState(EnumEntityState.SwimIdle);
            entity.RigidCompo.velocity = Vector2.zero;
            return;
        }
        PlayerMoveEvent @event = new PlayerMoveEvent(entity, inputReader.playerMoveDir.Value);
        entity.playerEventContainer.Execute(EnumPlayerEvents.MoveEvent, @event);
    }
    public override void ExitState()
    {
        base.ExitState();
        entity.RigidCompo.velocity = Vector2.zero;
    }
}
