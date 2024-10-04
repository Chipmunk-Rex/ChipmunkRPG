using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerEvent : BaseEvent
{
    protected Player player;
    public PlayerEvent(Player player)
    {
        this.player = player;
    }
}
