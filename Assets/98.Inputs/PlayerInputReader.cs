using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

public class PlayerInputReader : ScriptableSingleton<PlayerInputReader>, IPlayerActions
{
    Controls controls;
    public NotifyValue<Vector2> playerMoveDir = new();
    public event Action<bool> onItemUse;
    public event Action onInventory;
    public event Action<float> onWheel;
    public Vector2 mousePos { get; private set; }
    public Vector2 mouseWorldPos { get => Camera.main.ScreenToWorldPoint(mousePos); }


    protected override void OnEnable()
    {
        base.OnEnable();

        controls = new();

        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        playerMoveDir.Value = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
            onInventory?.Invoke();
    }

    public void OnWheel(InputAction.CallbackContext context)
    {
        if (context.performed)
            onWheel?.Invoke(context.ReadValue<float>());
    }

    public void OnItemUse(InputAction.CallbackContext context)
    {
        if (context.performed)
            onItemUse?.Invoke(true);
        if (context.canceled)
            onItemUse?.Invoke(false);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
            mousePos = context.ReadValue<Vector2>();

    }
}
