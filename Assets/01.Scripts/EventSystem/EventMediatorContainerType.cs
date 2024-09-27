using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMediatorContainerType : EventMediatorContainer<Type>
{
    public void Invoke<TKey>(BaseEvent @event) where TKey : BaseEvent
    {
        Execute(typeof(TKey), @event);
    }
    public void Subscribe<TKey>(Action<BaseEvent> handler) where TKey : BaseEvent
    {
        Subscribe(typeof(TKey), handler);
    }
    public void UnSubscribe<TKey>(Action<BaseEvent> handler) where TKey : BaseEvent
    {
        eventMediators[typeof(TKey)].UnSubscribe(handler);
    }
}
