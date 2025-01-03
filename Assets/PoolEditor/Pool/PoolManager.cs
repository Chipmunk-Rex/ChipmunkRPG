using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chipmunk.Library.PoolEditor
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        [SerializeField] List<PoolSO> poolSOList;
        private Dictionary<string, Pool> _poolContainer = new();
        bool isInitialized = false;
        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (isInitialized) return;
            isInitialized = true;
            foreach (PoolSO poolSO in poolSOList)
                CreatePool(poolSO);
        }

        protected override void Awake()
        {
            base.Awake();

        }

        private void CreatePool(PoolSO poolSO)
        {
            if (poolSOList == null) return;

            foreach (PoolItemSO poolItemSO in poolSO.itemList)
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
        public bool CheckPool(string poolName)
        {
            if (_poolContainer.ContainsKey(poolName))
                return true;
            return false;
        }
        #region Pop
        public GameObject Pop(string itemName)
        {
            Initialize();
            if (_poolContainer.ContainsKey(itemName))
            {
                GameObject item = _poolContainer[itemName].Pop();
                item.GetComponent<IPoolAble>().OnPoped();
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
                item.OnPushed();
                _poolContainer[item.PoolName].Push((item as MonoBehaviour).gameObject);
                return;
            }

            Debug.LogError($"There is no pool {item.PoolName}");
        }
        public void Push(GameObject item)
        {
            IPoolAble poolAble = item.GetComponent<IPoolAble>();
            if (poolAble == null)
            {
                Debug.LogError($"There is no IPoolAble in {item.name}");
                return;
            }
            Push(poolAble);
        }
        #endregion
    }
}