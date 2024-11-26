using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : BuildingEntity
{
    public bool isGrown
    {
        get
        {
            if (plantSO.growthTime == 0 || plantSO.growthSprites.Count == 0)
                return true;
            return liveTime / plantSO.growthTime >= plantSO.growthSprites.Count;
        }
    }
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
        World.Time.OnvalueChanged.AddListener(OnTimeChanged);


        RigidCompo.bodyType = RigidbodyType2D.Static;
    }
    public override void OnPushed()
    {
        base.OnPushed();
        World.Time.OnvalueChanged.RemoveListener(OnTimeChanged);

        RigidCompo.bodyType = RigidbodyType2D.Dynamic;
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
            if (plantSO.growthSprites.Count == 0)
                return;
            Visual.sprite = plantSO.growthSprites[plantSO.growthSprites.Count - 1];
        }
        else
        {
            if (plantSO.growthTime == 0)
            {
                return;
            }
            Visual.sprite = plantSO.growthSprites[time / plantSO.growthTime];
        }
    }

    public override void Die()
    {
    }
}
