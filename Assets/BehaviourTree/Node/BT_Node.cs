using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BT_Node : ScriptableObject
{
    [HideInInspector] public BT_EnumNodeState state = BT_EnumNodeState.Running;
    [HideInInspector] private bool isStarted = false;
    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 position;
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
    public abstract void AddChild(BT_Node child);
    public abstract void RemoveChild(BT_Node child);
    public abstract List<BT_Node> GetChild();
    public abstract BT_Node Clone();
    protected abstract BT_EnumNodeState OnNodeUpdate();
}
