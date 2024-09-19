using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInitializeable
{
    void Initialize();
}
public interface IInitializeable<T>
{
    T Initialize();
}
