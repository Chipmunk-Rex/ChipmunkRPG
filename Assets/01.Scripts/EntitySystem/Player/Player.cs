using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : Entity, IFSMEntity<EnumPlayerState, Player>
{

    public Animator Animator => animatorCompo;

    public bool CanChangeState => true;
    [field: SerializeField] public FSMStateMachine<EnumPlayerState, Player> FSMStateMachine { get; private set; } = new();
    public EventMediatorContainer<EnumPlayerEvents, PlayerEvent> playerEventContainer = new();
    public PlayerInputReader playerInputReader => PlayerInputReader.Instance;

    protected override void Awake()
    {
        base.Awake();

        SubscribeInput();

        InitializeStateMachine();
    }
    private void Update() {
        FSMStateMachine.UpdateState();
    }
    private void SubscribeInput()
    {
        playerInputReader.playerMoveDir.OnvalueChanged += OnMove;
    }

    private void OnMove(Vector2 prev, Vector2 next)
    {
        Debug.Log("wwww");
        FSMStateMachine.ChangeState(EnumPlayerState.Move);
    }

    public void InitializeStateMachine()
    {
        FSMStateMachine.AddState(EnumPlayerState.Idle, new PlayerIdleState(this, "Idle"));
        FSMStateMachine.AddState(EnumPlayerState.Move, new PlayerMoveState(this, "Move"));
        FSMStateMachine.Initailize(EnumPlayerState.Idle, this);
    }
}
