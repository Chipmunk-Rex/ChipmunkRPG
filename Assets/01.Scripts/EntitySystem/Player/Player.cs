using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Player : Entity, IFSMEntity<EnumEntityState, Player>, IItemInteractHandler
{

    public PlayerInputReader playerInputReader => PlayerInputReader.Instance;
    public UnityEvent inventoryOpenEvent;
    public FSMStateMachine<EnumEntityState, Player> FSMStateMachine { get; private set; } = new();
    public bool CanChangeState => true;
    public Animator Animator => AnimatorCompo;

    public Inventory Inventory { get; private set; } = new();
    public PlayerInventoryHotbar InventoryHotbar { get; private set; }
    int animXHash = Animator.StringToHash("X");
    int animYHash = Animator.StringToHash("Y");
    public EventMediatorContainer<EnumPlayerEvents, PlayerEvent> playerEventContainer = new();
    override public void OnEnable()
    {
        base.OnEnable();

    }
    public override Entity Initialize<T>(T entitySO)
    {
        base.Initialize(entitySO);
        Subscribe();
        Inventory.Initialize(new Item[20], new Vector2Int(7, 3), this, 5);
        InventoryHotbar = new PlayerInventoryHotbar(this, Inventory, 5);
        return this;
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
        InitializeStateMachine();
    }
    public override void Awake()
    {
        base.Awake();
    }
    public override void Update()
    {
        FSMStateMachine.UpdateState();
    }
    private void Subscribe()
    {
        playerInputReader.onInventory += OnOpenInventory;
        playerInputReader.onItemUse += OnItemUse;

        lookDir.OnvalueChanged += OnLookDirChanged;
    }

    private void OnItemUse(bool obj)
    {
        SetLookDirByMousePos();
    }
    public void SetLookDirByMousePos()
    {
        Vector2 dir = (playerInputReader.MouseWorldPos - (Vector2)transform.position).normalized;
        this.lookDir.Value = dir;
    }

    private void OnLookDirChanged(Vector2 prev, Vector2 next)
    {
        transform.localScale = new Vector3(-Mathf.Sign(next.x), transform.localScale.y, transform.localScale.z);
        Animator.SetFloat(animXHash, next.x);
        Animator.SetFloat(animYHash, next.y);
    }

    private void OnOpenInventory()
    {
        inventoryOpenEvent?.Invoke();
    }

    private void OnMove(Vector2 prev, Vector2 next)
    {
        FSMStateMachine.ChangeState(EnumEntityState.Move);
    }

    public void InitializeStateMachine()
    {
        Debug.Log(AnimatorCompo);
        FSMStateMachine.AddState(EnumEntityState.Idle, new PlayerIdleState(this, "Idle"));
        FSMStateMachine.AddState(EnumEntityState.Move, new PlayerMoveState(this, "Move"));
        FSMStateMachine.AddState(EnumEntityState.Build, new PlayerBuildState(this, "Build"));
        FSMStateMachine.AddState(EnumEntityState.Use, new PlayerBuildState(this, "Use"));
        FSMStateMachine.AddState(EnumEntityState.Eat, new PlayerBuildState(this, "Eat"));
        FSMStateMachine.Initailize(EnumEntityState.Idle, this);
    }
    public override NDSData Serialize()
    {
        NDSData entityNDSData = base.Serialize();
        entityNDSData.AddData("Inventory", Inventory.Serialize());
        return entityNDSData;
    }
    public override void Deserialize(NDSData data)
    {
        base.Deserialize(data);
        Inventory.Deserialize(data.GetData<NDSData>("Inventory"));
    }

    public void OnBeforeInteract(Item target)
    {
        ChangeStateByItem(target);
    }
    public void ChangeStateByItem(Item target)
    {
        if (target == null)
        {
            FSMStateMachine.ChangeState(EnumEntityState.Idle);
            return;
        }
        if (target is FoodItem)
        {
            FSMStateMachine.ChangeState(EnumEntityState.Eat);
        }
        else
        {
            FSMStateMachine.ChangeState(EnumEntityState.Use);
        }
    }
    public void OnInteract(Item target)
    {
    }

    public void OnEndInteract(Item target)
    {
        ChangeStateByItem(null);
    }
}
