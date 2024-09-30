using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolAble : MonoBehaviour, IPoolAble
    {
        [SerializeField] public UnityEvent onResetItem;
        public string poolName;
        public string PoolName => poolName;

        public GameObject ObjectPref => gameObject;

        public void ResetItem()
        {
            onResetItem?.Invoke();
        }
    }
}