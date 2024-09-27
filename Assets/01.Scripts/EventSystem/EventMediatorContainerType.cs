using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMediatorContainerType : BaseEventMediatorContainer<Type>
{
    public void Invoke<TKey>(BaseEvent @event) where TKey : BaseEvent
    {
        Invoke(typeof(TKey), @event);
    }

    public override void Invoke<TKey>(TKey type, BaseEvent @event)
    {
        eventMediators[type].Invoke(@event);
    }

    public void Subscribe<TKey>(Action<BaseEvent> handler) where TKey : BaseEvent
    {
        Subscribe(typeof(TKey), handler);
    }

    public override void Subscribe<TKey>(TKey type, Action<BaseEvent> handler)
    {
        if (!eventMediators.ContainsKey(type))
            eventMediators.Add(type, new EventMediator<BaseEvent>());

        eventMediators[type].Subscribe(handler);
    }

    public void UnSubscribe<TKey>(Action<BaseEvent> handler) where TKey : BaseEvent
    {
        eventMediators[typeof(TKey)].UnSubscribe(handler);
    }

    public override void UnSubscribe<TKey>(TKey type, Action<BaseEvent> handler)
    {
        if (!eventMediators.ContainsKey(type))
        {
            Debug.LogWarning("EventMediator is not Exsit");
            eventMediators.Add(type, new EventMediator<BaseEvent>());
        }
        eventMediators[type].UnSubscribe(handler);
    }
}
