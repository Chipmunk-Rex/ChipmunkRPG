using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_BehaviourTreeExecutor : MonoBehaviour
{
    [SerializeField] private BT_BehaviourTree behaviourTree;
    private BT_BehaviourTree behaviourTreeClone;
    private void Start()
    {
        behaviourTreeClone = behaviourTree.Clone();
    }
    private void Update()
    {
        behaviourTreeClone.UpdateTree();
    }
}
