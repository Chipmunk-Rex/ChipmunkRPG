using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "EntityData", menuName = "SO/EntityData")]
public class EntitySO : ScriptableObject
{
    public int maxHP;
    public int attackDamage;
    public float moveSpeed = 1;
    public float attackSpeed;
    public GameObject shadowPrefab;
    public RuntimeAnimatorController animatorController;
}
