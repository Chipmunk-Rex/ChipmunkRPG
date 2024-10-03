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

    public void OnAttact(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
