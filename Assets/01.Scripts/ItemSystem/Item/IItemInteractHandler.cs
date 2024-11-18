using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItemInteractHandler
{
    public void OnBeforeInteract(Item target);
    public void OnInteract(Item target);
    public void OnEndInteract(Item target, bool isCanceled);
}
