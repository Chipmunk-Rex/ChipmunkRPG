using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SOAddressData : JsonData<ScriptableObject, SOAddressData>
{
    public uint id;
    public override SOAddressData Serialize(ScriptableObject so)
    {
        this.id = SOAddressSO.Instance.GetIDBySO(so);
        return this;
    }

    public static implicit operator SOAddressData(ScriptableObject so)
    {
        return new SOAddressData().Serialize(so);
    }
    public static implicit operator ScriptableObject(SOAddressData soAddressData)
    {
        return SOAddressSO.Instance.GetSOByID(soAddressData.id);
    }
}
[Serializable]
public class SOAddressData<T> : SOAddressData where T : ScriptableObject
{
    public override SOAddressData Serialize(ScriptableObject so)
    {
        this.id = SOAddressSO.Instance.GetIDBySO(so);
        return this;
    }
    public static implicit operator SOAddressData<T>(T so)
    {
        return new SOAddressData<T>().Serialize(so) as SOAddressData<T>;
    }
    public static implicit operator T(SOAddressData<T> soAddressData)
    {
        return soAddressData == null ? null : SOAddressSO.Instance.GetSOByID(soAddressData.id) as T;
    }
}
