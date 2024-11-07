using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableItem
{
    public IInteractableItemSO interactableItemSO { get; }
    public void OnBeforeInteract(EntityCompo target);
    public void OnInteract(EntityCompo target);
    public void OnEndInteract(EntityCompo target);
}
