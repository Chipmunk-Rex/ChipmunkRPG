using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

public class UIInputReader : ScriptableSingleton<UIInputReader>, IUIActions
{
    public Action onCraft;
    Controls controls;
    protected override void OnEnable()
    {
        base.OnEnable();
        controls = new();
        controls.UI.SetCallbacks(this);
        controls.UI.Enable();
    }
    public void OnCraft(InputAction.CallbackContext context)
    {
        if (context.performed)
            onCraft?.Invoke();
    }

    public void OnSetting(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
