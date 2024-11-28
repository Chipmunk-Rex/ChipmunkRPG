using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chipmunk.Library;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class Player : Entity, IFSMEntity<EnumEntityState, Player>, IItemInteractHandler, IItemVisualable, IInventoryOwner, IItemCrafterEntity
{
    public PlayerInputReader playerInputReader => PlayerInputReader.Instance;
    public UnityEvent inventoryOpenEvent;
    public FSMStateMachine<EnumEntityState, Player> FSMStateMachine { get; private set; } = new();
    public bool CanChangeState => true;

    public Inventory Inventory { get; private set; } = new();
    public PlayerInventoryHotbar InventoryHotbar { get; private set; }

    public SpriteRenderer ItemSpriteCompo { get; private set; }

    public Animator ItemAnimatorCompo { get; private set; }

    public IItemCrafterSO ItemCrafterSO => EntitySO as IItemCrafterSO;

    public ItemCrafter ItemCrafter { get; private set; }

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
        ItemCrafter = new ItemCrafter(this);
        // ItemCrafter
        return this;
    }
    public override void OnSpawn()
    {
        InitializeStateMachine();

        // ItemSpriteCompo = new GameObject("ItemVisual").AddComponent<SpriteRenderer>();
        ItemSpriteCompo = Visual.transform.GetChild(0).GetComponent<SpriteRenderer>();
        ItemSpriteCompo.sortingLayerName = "Entity";
        ItemSpriteCompo.sortingOrder = 1;
        ItemSpriteCompo.transform.SetParent(Visual.transform);
        ItemAnimatorCompo = ItemSpriteCompo.gameObject.AddComponent<Animator>();

        if(World.Instance.GetGround(Vector2Int.RoundToInt(transform.position)).groundSO.isWater)
        {
            FSMStateMachine.ChangeState(EnumEntityState.SwimIdle);
        }
        else
        {
            FSMStateMachine.ChangeState(EnumEntityState.Idle);
        }

        base.OnSpawn();
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
        playerInputReader.onInteract += OnInteract;

        lookDir.OnvalueChanged += OnLookDirChanged;
    }

    private void OnInteract()
    {

        // 물 타일이면 수영
        Debug.Log(lookDir.Value);
        Vector2Int lookTilePos = Vector2Int.RoundToInt(transform.position + (Vector3)lookDir.Value.normalized);
        if (World.GetGround(lookTilePos).groundSO.isWater)
        {
            if (FSMStateMachine.CurrentEnumState != EnumEntityState.SwimMove && FSMStateMachine.CurrentEnumState != EnumEntityState.SwimIdle)
            {
                FSMStateMachine.ChangeState(EnumEntityState.SwimIdle);

                InventoryHotbar.canUseItem = false;
                Vector2Int lookWorldPos = Vector2Int.RoundToInt(transform.position + (Vector3)lookDir.Value.normalized);
                transform.position = new Vector3(lookWorldPos.x, lookWorldPos.y);
            }
        }
        else
        {

            if (FSMStateMachine.CurrentEnumState == EnumEntityState.SwimMove || FSMStateMachine.CurrentEnumState == EnumEntityState.SwimIdle)
            {
                FSMStateMachine.ChangeState(EnumEntityState.Idle);

                InventoryHotbar.canUseItem = true;
                Vector2Int lookWorldPos = Vector2Int.RoundToInt(transform.position + (Vector3)lookDir.Value.normalized);
                transform.position = new Vector3(lookWorldPos.x, lookWorldPos.y);
            }
        }

        Debug.Log("Interact");
        RaycastHit2D[] colliders = Physics2D.RaycastAll(transform.position, lookDir.Value, 1f);
        Entity[] entities = colliders.Select(e => e.collider.GetComponent<EntityCompo>().Entity).ToArray();
        foreach (Entity entity in entities)
        {
            if (entity != this)
            {
                entity.OnPlayerInteract(this);
                break;
            }
        }
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
        AnimatorCompo.SetFloat(animXHash, next.x);
        AnimatorCompo.SetFloat(animYHash, next.y);
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
        FSMStateMachine.AddState(EnumEntityState.Idle, new PlayerIdleState(this, "Idle"));
        FSMStateMachine.AddState(EnumEntityState.Move, new PlayerMoveState(this, "Move"));
        FSMStateMachine.AddState(EnumEntityState.Build, new PlayerBuildState(this, "Build"));
        FSMStateMachine.AddState(EnumEntityState.Use, new PlayerBuildState(this, "Use"));
        FSMStateMachine.AddState(EnumEntityState.Eat, new PlayerBuildState(this, "Eat"));
        FSMStateMachine.AddState(EnumEntityState.Die, new PlayerDeadState(this, "Die"));
        FSMStateMachine.AddState(EnumEntityState.SwimIdle, new PlayerSwimIdleState(this, "SwimIdle"));
        FSMStateMachine.AddState(EnumEntityState.SwimMove, new PlayerSwimState(this, "SwimMove"));
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

    public void OnBeforeInteractItem(Item target)
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
    public void OnInteractItem(Item target)
    {
    }

    public void OnEndInteractItem(Item target, bool isCanceled)
    {
        ChangeStateByItem(null);
    }

    public override void Die()
    {
        FSMStateMachine.ChangeState(EnumEntityState.Die);
    }
}
