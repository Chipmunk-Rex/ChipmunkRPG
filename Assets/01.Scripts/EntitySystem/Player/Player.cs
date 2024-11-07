using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Player : Entity, IFSMEntity<EnumPlayerState, Player>
{

    public PlayerInputReader playerInputReader => PlayerInputReader.Instance;
    public UnityEvent inventoryOpenEvent;
    public bool CanChangeState => true;
    [field: SerializeField] public FSMStateMachine<EnumPlayerState, Player> FSMStateMachine { get; private set; } = new();
    public Animator animator;
    public Animator Animator => animator;

    public EventMediatorContainer<EnumPlayerEvents, PlayerEvent> playerEventContainer = new();

    public override void Awake()
    {
        animator = GetComponent<Animator>();
        base.Awake();
        SubscribeInput();

        InitializeStateMachine();
    }
    public override void Update()
    {
        FSMStateMachine.UpdateState();
    }
    public override void FixedUpdate()
    {
        lookDir = (playerInputReader.mouseWorldPos - (Vector2)transform.position).normalized;
    }
    private void SubscribeInput()
    {
        playerInputReader.playerMoveDir.OnvalueChanged += OnMove;
        playerInputReader.onInventory += OnOpenInventory;
        playerInputReader.onItemUse += OnItemUse;
    }
    private void OnItemUse(bool obj)
    {
    }

    private void OnOpenInventory()
    {
        inventoryOpenEvent?.Invoke();
    }

    private void OnMove(Vector2 prev, Vector2 next)
    {
        FSMStateMachine.ChangeState(EnumPlayerState.Move);
    }

    public void InitializeStateMachine()
    {
        FSMStateMachine.AddState(EnumPlayerState.Idle, new PlayerIdleState(this, ""));
        FSMStateMachine.AddState(EnumPlayerState.Move, new PlayerMoveState(this, ""));
        FSMStateMachine.Initailize(EnumPlayerState.Idle, this);
    }
}
