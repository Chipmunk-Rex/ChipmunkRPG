using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedItem : StackableItem, IInteractableItem
{
    SeedSO seedSO;
    public SeedItem(BaseItemSO itemSO) : base(itemSO)
    {
        seedSO = itemSO as SeedSO;
    }

    public IInteractableItemSO interactableItemSO => seedSO;

    public bool VisualOnInteract { get; private set; }

    public void OnBeforeInteract(Entity target)
    {

    }

    public void OnEndInteract(Entity target, bool isCanceled)
    {
        if (isCanceled)
            return;

        Vector2Int plantPos = Vector2Int.RoundToInt(target.lookDir.Value + (Vector2)target.transform.position);

        BuildingSO buildingSO = ScriptableObject.CreateInstance<BuildingSO>();
        buildingSO.buildingEntity = seedSO.plantSO;

        if (target.currentWorld.CanBuild(plantPos))
        {
            target.currentWorld.CreateBuilding(plantPos, buildingSO);
            RemoveStack(target);
        }
    }

    public void OnInteract(Entity target)
    {
    }
}
