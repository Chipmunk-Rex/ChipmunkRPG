using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Animal : Entity
{
    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Entity[] entities = DetectEntities();
        Entity player = entities.Where(e => e is Player).FirstOrDefault();
        if (player != null)
        {
            EntityMoveEvent entityEvent = new EntityMoveEvent(this, transform.position - player.transform.position);
            this.EntityEvents.Execute(EnumEntityEvent.Move, entityEvent);
        }
        else
        {
            EntityMoveEvent entityEvent = new EntityMoveEvent(this, Vector3.zero);
            this.EntityEvents.Execute(EnumEntityEvent.Move, entityEvent);
        }
    }
    public override void Die()
    {

    }
}
