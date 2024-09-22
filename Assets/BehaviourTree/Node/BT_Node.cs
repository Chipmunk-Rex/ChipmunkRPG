using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BT_Node : ScriptableObject
{
    public BT_EnumNodeState state = BT_EnumNodeState.Running;
    private bool isStarted = false;
    internal string guid;

    public BT_EnumNodeState UpdateNode()
    {
        if (!isStarted)
        {
            OnNodeStart();
            isStarted = true;
        }

        state = OnNodeUpdate();

        if (state != BT_EnumNodeState.Running)
        {
            OnNodeStop();
            isStarted = false;
        }

        return state;
    }
    public abstract void OnNodeStart();
    public abstract void OnNodeStop();
    protected abstract BT_EnumNodeState OnNodeUpdate();
}
