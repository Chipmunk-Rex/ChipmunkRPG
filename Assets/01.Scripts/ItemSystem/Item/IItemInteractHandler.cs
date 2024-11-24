using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemInteractHandler
{
    public void OnBeforeInteractItem(Item target);
    public void OnInteractItem(Item target);
    public void OnEndInteractItem(Item target, bool isCanceled);
}
