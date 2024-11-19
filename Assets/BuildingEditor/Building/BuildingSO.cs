using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "SO/TEstBuilding")]
public class BuildingSO : ScriptableObject
{
    [HideInInspector] public SerializableDictionary<Vector2Int, TileBase> tileDatas = new();
    /// <summary>
    /// top right left buttom
    /// </summary>
    [HideInInspector] public (int, int, int, int) tileDataSize => (top, right, down, left);
    public EntitySO buildingEntity;
    [Header("Size")]
    [SerializeField] public int top;
    [SerializeField] public int right;
    [SerializeField] public int down;
    [SerializeField] public int left;
    
}
