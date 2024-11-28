using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Chipmunk.Library;
using UnityEngine;
public class Animal : Entity, IFSMEntity<EnumEntityState, Animal>
{
    public bool CanChangeState => true;

    public FSMStateMachine<EnumEntityState, Animal> FSMStateMachine { get; private set; }

    public override void Update()
    {
        base.Update();
        FSMStateMachine.UpdateState();
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
        FSMStateMachine = new FSMStateMachine<EnumEntityState, Animal>();
        FSMStateMachine.AddState(EnumEntityState.Idle, new Animal_IdleState(this, "Idle"));
        FSMStateMachine.AddState(EnumEntityState.Move, new Animal_RunAwayState(this, "Move"));
        FSMStateMachine.Initailize(EnumEntityState.Idle, this);
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

    public void InitializeStateMachine()
    {
        throw new System.NotImplementedException();
    }
}
