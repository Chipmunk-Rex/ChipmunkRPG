using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public EventMediatorContainer<EnumPlayerEvents, PlayerEvent> playerEventContainer = new();

    // protected override void Awake()
    // {
    //     base.Awake();
    //     SubscribeInput();

    //     InitializeStateMachine();
    // }
    // void FixedUpdate()
    // {
    //     lookDir = (playerInputReader.mouseWorldPos - (Vector2)transform.position).normalized;
    // }
    // private void SubscribeInput()
    // {
    //     playerInputReader.playerMoveDir.OnvalueChanged += OnMove;
    //     playerInputReader.onInventory += OnOpenInventory;
    //     playerInputReader.onItemUse += OnItemUse;
    // }
    // private void OnItemUse(bool obj)
    // {
    // }

    // private void OnOpenInventory()
    // {
    //     inventoryOpenEvent?.Invoke();
    // }

    // private void OnMove(Vector2 prev, Vector2 next)
    // {
    //     FSMStateMachine.ChangeState(EnumPlayerState.Move);
    // }

    // public void InitializeStateMachine()
    // {
    //     FSMStateMachine.AddState(EnumPlayerState.Idle, new PlayerIdleState(this, ""));
    //     FSMStateMachine.AddState(EnumPlayerState.Move, new PlayerMoveState(this, ""));
    //     FSMStateMachine.Initailize(EnumPlayerState.Idle, this);
    // }
}
