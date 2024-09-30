using System;
using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library.PoolEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] BuildingSO so;
    private void Start()
    {
        UnityEngine.Random.InitState(-1);
        Debug.Log(UnityEngine.Random.Range(0, int.MaxValue));
        UnityEngine.Random.InitState(-1);
        Debug.Log(UnityEngine.Random.Range(0, int.MaxValue));
        // GameObject gameObject = PoolManager.Instance.Pop("ww");
        // PoolManager.Instance.Pop("ww");
        // gameObject.name = "ming";
        // PoolManager.Instance.Push(gameObject);
        StartCoroutine(enumerator());
    }
    IEnumerator enumerator()
    {
        yield return null;
        BuildingManager.Instance.eventContainer.Subscribe(EnumBuildingEvent.CreateBuilding, CreateBuildingHandler);
        BuildingManager.Instance.ConstructBuilding(Vector2Int.RoundToInt(transform.position), new BaseBuilding(so));
    }
    private void CreateBuildingHandler(BaseEvent @event)
    {
        @event.onAfterExcute += OnAfterExecute;
        CreateBuildingEvent createBuildingEvent = @event as CreateBuildingEvent;
        Debug.Log("건설 시도");
    }

    private void OnAfterExecute(EnumEventResult result)
    {
        Debug.Log("건설 시도");
    }
}
