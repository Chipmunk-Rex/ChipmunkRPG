using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chipmunk.Library.PoolEditor
{
    public class Pool : MonoBehaviour
    {
        private PoolItemSO _poolItemSO;
        private Stack<GameObject> _objectPool = new();
        public void Initialize(PoolItemSO poolItemSO)
        {
            _poolItemSO = poolItemSO;
            this.name = $"Pool ({poolItemSO.poolName})";

            CreatePool(poolItemSO);
        }
        private void CreatePool(PoolItemSO poolItemSO)
        {
            GameObject objPref = poolItemSO.prefab;

            for (int i = 0; i < poolItemSO.count; i++)
            {
                GameObject gameObj = GameObject.Instantiate(objPref, this.transform);
                gameObj.name = poolItemSO.poolName;

                Push(gameObj);
            }
        }
        public GameObject Pop()
        {
            GameObject item = null;
            if (_objectPool.Count == 0)
            {
                GameObject gameObj = GameObject.Instantiate(_poolItemSO.prefab, this.transform);
                gameObj.name = _poolItemSO.poolName;
            }
            else
            {
                item = _objectPool.Pop();
                item.SetActive(true);
            }
            return item;
        }
        public void Push(GameObject item)
        {
            item.SetActive(false);
            _objectPool.Push(item);
        }
    }
}