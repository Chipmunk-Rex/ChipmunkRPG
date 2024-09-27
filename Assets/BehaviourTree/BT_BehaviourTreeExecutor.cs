using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_BehaviourTreeExecutor : MonoBehaviour
{
    [HideInInspector] public BT_BehaviourTree behaviourTreeClone { get; private set; }
    [SerializeField] private BT_BehaviourTree behaviourTree;
    private void Start()
    {
        behaviourTreeClone = behaviourTree.Clone();
    }
    private void Update()
    {
        behaviourTreeClone.UpdateTree();
    }
}
