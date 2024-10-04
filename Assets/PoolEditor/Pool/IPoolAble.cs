using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chipmunk.Library.PoolEditor
{
    public interface IPoolAble
    {
        public string PoolName { get; }
        public GameObject ObjectPref { get; }
        public void InitializeItem();
        public void ResetItem();
    }
}