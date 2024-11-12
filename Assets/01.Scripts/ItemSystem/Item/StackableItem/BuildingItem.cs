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
    }

    public void OnEndInteract(Entity target)
    {
        Vector2 position = (Vector2)target.transform.position + target.lookDir.Value;
        Building building = new Building(buildingItemSO.buildingSO);
        WorldEvent @event = new CreateBuildingEvent(target.currentWorld, building, Vector2Int.RoundToInt(position));
        target.currentWorld.worldEvents.Execute(EnumWorldEvent.BuildingCreate, @event);

    }

    public void OnInteract(Entity target)
    {
    }
}
