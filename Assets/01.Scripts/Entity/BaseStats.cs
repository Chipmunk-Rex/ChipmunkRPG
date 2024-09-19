using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class BaseStats : MonoBehaviour, IInitializeable
{
    public abstract void Initialize();
}
