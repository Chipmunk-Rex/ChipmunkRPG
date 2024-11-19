using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Entity
{
    public bool isGrown => liveTime / plantSO.growthTime >= plantSO.growthSprites.Count;
    PlantSO plantSO;
    public int liveTime;
    public override Entity Initialize<T>(T entitySO)
    {
        this.plantSO = entitySO as PlantSO;
        return base.Initialize(entitySO);
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
        currentWorld.Time.OnvalueChanged.AddListener(OnTimeChanged);
    }

    private void OnTimeChanged(int arg0, int arg1)
    {
        liveTime++;
        GrowPlant(liveTime);
    }
    public void GrowPlant(int time)
    {
        if (isGrown)
        {
            Visual.sprite = plantSO.growthSprites[plantSO.growthSprites.Count - 1];
        }
        else
        {
            Visual.sprite = plantSO.growthSprites[time / plantSO.growthTime];
        }
    }

    public override void Die()
    {
    }
}
