using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;

public class PlayerMoveState : FSMState<EnumPlayerState, Player>
{
    PlayerInputReader inputReader => PlayerInputReader.Instance;
    public PlayerMoveState(IFSMEntity<EnumPlayerState, Player> entity, string animName) : base(entity, animName)
    {
    }

    public override void UpdateState()
    {
        PlayerMoveEvent @event = new PlayerMoveEvent(entity, inputReader.playerMoveDir.Value);
        entity.playerEventContainer.Execute(EnumPlayerEvents.MoveEvent, @event);
        if(inputReader.playerMoveDir.Value == Vector2.zero)
        {
            entity.FSMStateMachine.ChangeState(EnumPlayerState.Idle);
        }
    }
}
