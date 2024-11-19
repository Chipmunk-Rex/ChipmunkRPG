using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSO : EntitySO
{
    [Tooltip("식물이 자랄때 보여줄 스프라이트들")]
    public List<Sprite> growthSprites;
    [Tooltip("식물이 죽었을때 보여줄 스프라이트")]
    public Sprite deadSprite;
    [Tooltip("식물이 온전히 성장할때까지 걸리는 시간(초)")]
    public int growthTime;
    [Tooltip("식물을 수확했을때 드랍되는 아이템")]
    public BaseItemSO dropItem;
    protected override Entity CreateEntityInstance()
    {
        Plant plant = new Plant();
        return plant;
    }
}
