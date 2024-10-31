using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class JsonData<TInput, TReturn>
{
    public abstract TReturn Serialize(TInput data);

}