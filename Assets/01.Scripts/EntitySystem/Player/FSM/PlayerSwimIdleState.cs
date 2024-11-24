using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;

public class PlayerSwimIdleState : FSMState<EnumEntityState, Player>
{
    public PlayerSwimIdleState(IFSMEntity<EnumEntityState, Player> entity, string animName) : base(entity, animName)
    {
    }

    private void OnMove(Vector2 prev, Vector2 next)
    {
        if (next != Vector2.zero)
        {
            entity.FSMStateMachine.ChangeState(EnumEntityState.SwimMove);
        }
    }
    public override bool CanChangeTo(EnumEntityState targetState)
    {
        if (targetState == EnumEntityState.Idle || targetState == EnumEntityState.SwimMove)
            return true;
        return false;
    }
    public override void EnterState()
    {
        base.EnterState();
        PlayerInputReader.Instance.playerMoveDir.OnvalueChanged += OnMove;
    }
    public override void ExitState()
    {
        base.ExitState();
        PlayerInputReader.Instance.playerMoveDir.OnvalueChanged -= OnMove;
    }

    public override void UpdateState()
    {
    }
}
