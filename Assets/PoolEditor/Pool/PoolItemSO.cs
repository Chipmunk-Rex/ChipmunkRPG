using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItemSO : MonoBehaviour
{
    public string poolName;
    public GameObject prefab;
    public int count;
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
            }
        }
    }
}
