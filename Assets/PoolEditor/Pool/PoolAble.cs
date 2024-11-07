using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolAble : MonoBehaviour, IPoolAble
    {
        [SerializeField] public UnityEvent onResetItem;
        [SerializeField] public UnityEvent onInitializeItem;
        public string poolName;
        public string PoolName => poolName;

        public GameObject ObjectPref => gameObject;

        public void OnPoped()
        {
            onInitializeItem?.Invoke();
        }

        public void OnPushed()
        {
            onResetItem?.Invoke();
        }
    }
}