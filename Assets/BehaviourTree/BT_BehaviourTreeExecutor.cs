using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BT_BehaviourTreeExecutor : MonoBehaviour
{
    [SerializeField] private BT_BehaviourTree behaviourTree;
    private void Start()
    {
        behaviourTree = ScriptableObject.CreateInstance<BT_BehaviourTree>();

        // BT_Node node = ScriptableObject.CreateInstance<
    }
    private void Update()
    {

    }
}
