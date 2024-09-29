using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        [SerializeField] PoolSO poolList;
        private Dictionary<string, Pool> _poolContainer = new();
        protected override void Awake()
        {
            base.Awake();

            CreatePool();
        }

        private void CreatePool()
        {
            if (poolList == null) return;

            foreach (PoolItemSO poolItemSO in poolList.itemList)
            {
                if (_poolContainer.ContainsKey(poolItemSO.poolName))
                {
                    Debug.LogError($"PoolName {poolItemSO.poolName} is Already Exsit");
                    return;
                }

                GameObject poolGameObj = new GameObject();
                Pool pool = poolGameObj.AddComponent<Pool>();
                pool.transform.parent = this.transform;
                pool.Initialize(poolItemSO);

                _poolContainer.Add(poolItemSO.poolName, pool);
            }
        }
        #region Pop
        public GameObject Pop(string itemName)
        {
            if (_poolContainer.ContainsKey(itemName))
            {
                GameObject item = _poolContainer[itemName].Pop();
                item.GetComponent<IPoolAble>().ResetItem();
                return item;
            }
            Debug.LogError($"There is no pool {itemName}");
            return null;
        }

        public GameObject Pop(string itemName, Vector2 pos)
        {
            GameObject item = Pop(itemName);
            if (item != null)
                item.transform.position = pos;
            return item;
        }
        #endregion
        #region Push
        public void Push(IPoolAble item)
        {
            if (_poolContainer.ContainsKey(item.PoolName))
            {
                _poolContainer[item.PoolName].Push((item as MonoBehaviour).gameObject);
                return;
            }

            Debug.LogError($"There is no pool {item.PoolName}");
        }
        public void Push(GameObject item)
        {
            IPoolAble poolAble = item.GetComponent<IPoolAble>();
            if (_poolContainer.ContainsKey(poolAble.PoolName))
            {
                _poolContainer[poolAble.PoolName].Push(item);
                return;
            }

            Debug.LogError($"There is no pool {poolAble.PoolName}");
        }
        #endregion
    }
}