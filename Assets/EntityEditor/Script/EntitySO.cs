using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Chipmunk.Library;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "EntityData", menuName = "SO/EntityData")]
public abstract class EntitySO : ScriptableObject
{
    public string entityName;
    public Sprite defaultSprite;
    public GameObject shadowPrefab;
    public RuntimeAnimatorController animatorController;
    public abstract Entity CreateEntity();
}
