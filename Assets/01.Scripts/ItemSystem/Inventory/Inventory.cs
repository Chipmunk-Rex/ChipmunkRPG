using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : ItemContainer
{
    #region getter
    #endregion
    public Entity Owner { get; private set; }
    public void Initialize(Item[] items, Vector2Int containerSize, Entity owner, int hotbarSize)
    {
        base.Initialize(items, containerSize);
        this.Owner = owner;
    }
    public sealed override void Initialize(Item[] items, Vector2Int containerSize)
    {
        base.Initialize(items, containerSize);
    }
}
