using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/Entity/PlayerSO", order = 1)]
public class PlayerSO : EntitySO
{
    public override Entity CreateEntity()
    {
        return new Player();
    }
}
