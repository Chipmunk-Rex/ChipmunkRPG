using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chipmunk.Library;

namespace Chipmunk.Library
{
    [Serializable]
    public class FSMStateMachine<TEnumState, TEntity>  where TEnumState : Enum where TEntity : IFSMEntity<TEnumState, TEntity>
    {
        public FSMState<TEnumState, TEntity> CurrentState { get; private set; }
        [field : SerializeField]public TEnumState CurrentEnumState { get; private set; }
        public TEntity entity;
        Dictionary<TEnumState, FSMState<TEnumState, TEntity>> stateDictionary = new();
        /// <summary>
        /// 초기화함
        /// </summary>
        /// <param name="startState"></param>
        /// <param name="fsmEntity"></param>
        public void Initailize(TEnumState startState, TEntity fsmEntity)
        {
            this.entity = fsmEntity;
            CurrentState = stateDictionary[startState];
            CurrentState.EnterState();
        }
        /// <summary>
        /// State를 변경함
        /// </summary>
        /// <param name="state"></param>
        /// <exception cref="System.Exception"></exception>
        public void ChangeState(TEnumState state)
        {
            if (!stateDictionary.ContainsKey(state)) throw new System.Exception($"{state}는 상태머신에 존재하지 않습니다");
            if (!CurrentState.CanChangeTo(state)) return;
            if (!stateDictionary[state].CanChangeToThis(CurrentEnumState)) return;
            if (!entity.CanChangeState) return;

            CurrentState.ExitState();
            CurrentState = stateDictionary[state];
            CurrentEnumState = state;
            CurrentState.EnterState();
        }
        public void AddState(TEnumState state, FSMState<TEnumState, TEntity> entityState)
        {
            stateDictionary.Add(state, entityState);
        }

        internal void UpdateState()
        {
            CurrentState.UpdateState();
        }
    }
}