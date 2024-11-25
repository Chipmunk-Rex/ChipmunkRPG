using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/Entity/PlayerSO", order = 1)]
public class PlayerSO : EntitySO, IItemCrafterSO
{
    [field: SerializeField] public CraftRecipesTableSO CraftRecipesTable { get; private set; }

    protected override Entity CreateEntityInstance()
    {
        return new Player();
    }
}
