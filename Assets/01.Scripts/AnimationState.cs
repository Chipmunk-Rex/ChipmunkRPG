using System.Collections;
using System.Collections.Generic;
using Chipmunk.Library;
using UnityEngine;

public class AnimationState<TEntity> : FSMState<EnumEntityState, TEntity> where TEntity : IFSMEntity<EnumEntityState, TEntity>
{
    public AnimationState(IFSMEntity<EnumEntityState, TEntity> entity, string animName) : base(entity, animName)
    {
    }

    public override void UpdateState()
    {
    }
}