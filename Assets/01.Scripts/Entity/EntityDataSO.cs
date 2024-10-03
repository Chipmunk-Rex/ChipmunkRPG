using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "SO/EntityData")]
public class EntitySO : ScriptableObject
{
    public int maxHP;
    public int attackDamage;
    public int moveSpeed;
    public int attackSpeed;
}
