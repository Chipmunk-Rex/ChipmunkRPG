using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractableItem
{
    public IInteractableItemSO interactableItemSO { get; }
    public void OnBeforeInteract(Entity target);
    public void OnInteract(Entity target);
    public void OnEndInteract(Entity target);
}
