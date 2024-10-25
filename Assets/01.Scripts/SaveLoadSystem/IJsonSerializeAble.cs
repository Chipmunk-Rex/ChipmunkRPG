using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJsonSerializeAble
{
    public Object OnDeSerialize();
    public string OnSerialize();
}
