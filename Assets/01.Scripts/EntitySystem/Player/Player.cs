using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Player : Entity, IFSMEntity<EnumPlayerState, Player>
{

    #region getter
    public Animator Animator => animatorCompo;
    public PlayerInputReader playerInputReader => PlayerInputReader.Instance;
    #endregion;
    #region Event
    public UnityEvent inventoryOpenEvent;
    #endregion
    [SerializeField] public Inventory inventory;
    public bool CanChangeState => true;
    [field: SerializeField] public FSMStateMachine<EnumPlayerState, Player> FSMStateMachine { get; private set; } = new();
    public EventMediatorContainer<EnumPlayerEvents, PlayerEvent> playerEventContainer = new();

    protected override void Awake()
    {
        base.Awake();
        inventory.world = currentWorld;

        SubscribeInput();

        InitializeStateMachine();
    }
    private void Update()
    {
        FSMStateMachine.UpdateState();
    }
    void FixedUpdate()
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
