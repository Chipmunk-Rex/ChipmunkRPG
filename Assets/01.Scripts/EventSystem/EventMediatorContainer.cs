using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMediatorContainer<TBaseKey>
{
    protected Dictionary<TBaseKey, EventMediator<BaseEvent>> eventMediators = new();
    public virtual void Subscribe<TKey>(TKey key, Action<BaseEvent> handler) where TKey : TBaseKey
    {
        if (!eventMediators.ContainsKey(key))
            eventMediators.Add(key, new EventMediator<BaseEvent>());

        eventMediators[key].Subscribe(handler);
    }
    public virtual void UnSubscribe<TKey>(TKey key, Action<BaseEvent> handler) where TKey : TBaseKey
    {
        if (!eventMediators.ContainsKey(key))
        {
            Debug.LogWarning("EventMediator is not Exsit");
            eventMediators.Add(key, new EventMediator<BaseEvent>());
        }
        eventMediators[key].UnSubscribe(handler);
    }
    // public abstract void Invoke<TKey>(BaseEvent @event) where TKey : TBaseKey;
    public virtual void Execute<TKey>(TKey key, BaseEvent @event) where TKey : TBaseKey
    {
        if (!eventMediators.ContainsKey(key))
            eventMediators.Add(key, new EventMediator<BaseEvent>());

        eventMediators[key].Execute(@event);
    }
}
