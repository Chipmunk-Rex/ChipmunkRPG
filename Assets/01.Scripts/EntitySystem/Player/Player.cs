using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Player : Entity, IFSMEntity<EnumPlayerState, Player>
{
    public static Action<Player> OnPlayerCreated;

    public PlayerInputReader playerInputReader => PlayerInputReader.Instance;
    public UnityEvent inventoryOpenEvent;
    public FSMStateMachine<EnumPlayerState, Player> FSMStateMachine { get; private set; } = new();
    public bool CanChangeState => true;
    public Animator animator;
    public Animator Animator => animator;

    public Inventory Inventory { get; private set; } = new();

    public EventMediatorContainer<EnumPlayerEvents, PlayerEvent> playerEventContainer = new();
    override public void OnEnable()
    {
        base.OnEnable();
        animator = GetComponent<Animator>();
        SubscribeInput();

        InitializeStateMachine();
        Inventory.Initialize(new Item[20], new Vector2Int(7, 3), this,5);
        OnPlayerCreated?.Invoke(this);
    }
    public override void Awake()
    {
        base.Awake();
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
