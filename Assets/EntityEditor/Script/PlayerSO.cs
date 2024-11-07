using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSO : EntitySO
{
    public override Entity CreateEntity()
    {
        return new Player();
    }
}
