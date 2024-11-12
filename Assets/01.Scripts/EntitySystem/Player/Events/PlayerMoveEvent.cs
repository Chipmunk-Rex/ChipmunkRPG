using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveEvent : PlayerEvent
{
    public Vector2 moveDir;
    public PlayerMoveEvent(Player player, Vector2 moveDir) : base(player)
    {
        this.moveDir = moveDir;
    }

    public override EnumEventResult ExcuteEvent()
    {
        player.RigidCompo.velocity = moveDir * player.EntitySO.speed;
        player.lookDir.Value = moveDir;
        return EnumEventResult.Successed;
    }
}
