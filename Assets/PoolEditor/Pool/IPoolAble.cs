using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chipmunk.Library.PoolEditor
{
    public interface IPoolAble
    {
        public string PoolName { get; }
        public GameObject ObjectPref { get; }
        /// <summary>
        /// Called when pop from pool
        /// </summary>
        public void OnPoped();
        /// <summary>
        /// Called when go to pool
        /// </summary>
        public void OnPushed();
    }
}