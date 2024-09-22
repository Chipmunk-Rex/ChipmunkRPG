
using System;
using UnityEngine;

public class MonoSingletonn<T> : MonoBehaviour where T : MonoSingletonn<T>
{
    [Header("MonoSingleton")]
    [Tooltip("Dont Destroy on Scene Change")]
    [SerializeField]
    private bool isDontDestroy;

    [Space(1f)]
    protected static T _instace;

    private static bool IsDestroyed;

    public static T Instance
    {
        get
        {
            if (IsDestroyed)
            {
                _instace = null;
            }

            if ((UnityEngine.Object)_instace == (UnityEngine.Object)null)
            {
                _instace = UnityEngine.Object.FindAnyObjectByType<T>();
                if ((UnityEngine.Object)_instace == (UnityEngine.Object)null)
                {
                    throw new Exception("바보같은! 싱글톤이 없어!");
                }

                IsDestroyed = false;
            }

            return _instace;
        }
    }

    protected virtual void Awake()
    {
        if ((UnityEngine.Object)_instace == (UnityEngine.Object)null)
        {
            _instace = this as T;
            if (isDontDestroy)
            {
                UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
            }
        }
        else if (_instace != this)
        {
            UnityEngine.Object.Destroy(base.gameObject);
        }
        else
        {
            UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
        }
    }

    private void OnDisable()
    {
        IsDestroyed = true;
    }
}