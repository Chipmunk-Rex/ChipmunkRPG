using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingItem : StackableItem, IInteractableItem
{
    
    public BuildingItem(BaseItemSO itemSO) : base(itemSO)
    {
        buildingItemSO = itemSO as BuildingItemSO;
    }
    BuildingItemSO buildingItemSO;
    public IInteractableItemSO interactableItemSO => buildingItemSO;

    public void OnBeforeInteract(Entity target)
    {
        throw new System.NotImplementedException();
    }

    public void OnEndInteract(Entity target)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(Entity target)
    {
    }
}
