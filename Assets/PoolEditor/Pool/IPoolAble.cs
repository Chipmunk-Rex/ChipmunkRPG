using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolAble
{
    public string PoolName { get; }
    public GameObject ObjectPref { get; }
    public void ResetItem();
}
