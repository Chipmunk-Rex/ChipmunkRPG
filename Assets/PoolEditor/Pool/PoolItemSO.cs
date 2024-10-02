using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolItemSO : ScriptableObject
    {
        public string poolName;
        public GameObject prefab;
        public IPoolAble poolItem { get; private set; }
        public int count = 1;
        private void OnValidate()
        {
            if (prefab != null)
            {
                IPoolAble item = prefab.GetComponent<IPoolAble>();

                if (item == null)
                {
                    Debug.LogWarning("Can't find IPoolable script on prefab : check! " + prefab.name);
                    prefab = null;
                }
                else
                {
                    poolName = item.PoolName;
                    poolItem = item;
                }
            }


        }
    }
}