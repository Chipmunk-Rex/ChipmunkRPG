using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;

public class Squirrel : Entity, IFSMEntity<EnumEntityState, Squirrel>
{

    public bool CanChangeState => throw new System.NotImplementedException();

    public FSMStateMachine<EnumEntityState, Squirrel> FSMStateMachine => throw new System.NotImplementedException();

    public override void Die()
    {
    }

    public void InitializeStateMachine()
    {
        throw new System.NotImplementedException();
    }
}
