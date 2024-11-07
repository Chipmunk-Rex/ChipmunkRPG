using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INDSerializeAble
{
    public NDSData Serialize();
    public void Deserialize(NDSData data);
}
