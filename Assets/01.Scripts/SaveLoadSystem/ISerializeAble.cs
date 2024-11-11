using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerializeAble
{
    public object Serialize();
    public void Deserialize(object data);
}
