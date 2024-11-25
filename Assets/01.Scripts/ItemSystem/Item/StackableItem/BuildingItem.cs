using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingItem : Item, IInteractableItem
{

    public BuildingItem(BaseItemSO itemSO) : base(itemSO)
    {
        buildingItemSO = itemSO as BuildingItemSO;
    }
    BuildingItemSO buildingItemSO;
    public IInteractableItemSO interactableItemSO => buildingItemSO;

    [field: SerializeField] public bool VisualOnInteract { get; private set; }

    public void OnBeforeInteract(Entity target)
    {
    }

    public void OnEndInteract(Entity target, bool isCanceled)
    {
        if (isCanceled)
            return;
        Vector2 position = (Vector2)target.transform.position + target.lookDir.Value;
        Building building = buildingItemSO.buildingSO.CreateBuilding();
        WorldEvent @event = new CreateBuildingEvent(target.World, building, Vector2Int.RoundToInt(position));
        target.World.worldEvents.Execute(EnumWorldEvent.BuildingCreate, @event);

    }

    public void OnInteract(Entity target)
    {
    }
}
