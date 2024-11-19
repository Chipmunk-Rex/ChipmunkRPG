using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;
public class PlayerIdleState : FSMState<EnumEntityState, Player>
{
    public PlayerIdleState(IFSMEntity<EnumEntityState, Player> entity, string animName) : base(entity, animName)
    {
    }
    public override void EnterState()
    {
        base.EnterState();
        entity.RigidCompo.velocity = Vector2.zero;

        PlayerInputReader.Instance.playerMoveDir.OnvalueChanged += OnMove;
    }

    private void OnMove(Vector2 prev, Vector2 next)
    {
        if (next != Vector2.zero)
        {
            entity.FSMStateMachine.ChangeState(EnumEntityState.Move);
        }
    }

    public override void UpdateState()
    {

    }
    public override void ExitState()
    {
        base.ExitState();
        PlayerInputReader.Instance.playerMoveDir.OnvalueChanged -= OnMove;
    }
}
