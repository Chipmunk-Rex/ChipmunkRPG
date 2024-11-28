using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSO : EntitySO
{
    protected override Entity CreateEntityInstance()
    {
        return new Animal().Initialize(this);
    }
}
