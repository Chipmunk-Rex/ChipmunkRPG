using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;

public class PlayerMoveState : FSMState<EnumEntityState, Player>
{
    PlayerInputReader inputReader => PlayerInputReader.Instance;
    public PlayerMoveState(IFSMEntity<EnumEntityState, Player> entity, string animName) : base(entity, animName)
    {
    }

    public override void UpdateState()
    {
        if (inputReader.playerMoveDir.Value == Vector2.zero)
        {
            entity.FSMStateMachine.ChangeState(EnumEntityState.Idle);
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
