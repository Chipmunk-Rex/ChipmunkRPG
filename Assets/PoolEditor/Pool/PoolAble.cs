using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolAble : MonoBehaviour, IPoolAble
{
    public string poolName;
    public string PoolName => poolName;

    public GameObject ObjectPref => gameObject;

    public void ResetItem()
    {
    }
}
