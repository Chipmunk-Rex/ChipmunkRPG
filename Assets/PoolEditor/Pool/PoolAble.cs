using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolAble : MonoBehaviour, IPoolAble
    {
        public string poolName;
        public string PoolName => poolName;

        public GameObject ObjectPref => gameObject;

        public void ResetItem()
        {
        }
    }
}