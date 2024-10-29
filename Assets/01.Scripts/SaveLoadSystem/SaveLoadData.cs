using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveLoadData<T>
{
    protected abstract void OnConversion(T value);
    protected abstract T OnConversion();
    public static implicit operator T(SaveLoadData<T> v){
        return v.OnConversion();
    }
}
