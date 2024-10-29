using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SOAddressData
{
    public uint id;
    public ScriptableObject SO => SOAddressSO.Instance.GetSOByID(id);
    public SOAddressData(ScriptableObject so)
    {
        this.id = SOAddressSO.Instance.GetIDBySO(so);
    }
    public static implicit operator SOAddressData(ScriptableObject so)
    {
        return new SOAddressData(so);
    }
    public static implicit operator ScriptableObject(SOAddressData soAddressData)
    {
        return soAddressData.SO;
    }
}
