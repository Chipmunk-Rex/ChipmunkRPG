using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMediatorContainer<TBaseKey, TEvent> where TEvent : BaseEvent
{
    protected Dictionary<TBaseKey, EventMediator<TEvent>> eventMediators = new();
    public virtual void Subscribe<TKey>(TKey key, Action<TEvent> handler) where TKey : TBaseKey
    {
        if (!eventMediators.ContainsKey(key))
            eventMediators.Add(key, new EventMediator<TEvent>());

        eventMediators[key].Subscribe(handler);
    }
    public virtual void UnSubscribe<TKey>(TKey key, Action<TEvent> handler) where TKey : TBaseKey
    {
        if (!eventMediators.ContainsKey(key))
        {
            Debug.LogWarning("EventMediator is not Exsit");
            eventMediators.Add(key, new EventMediator<TEvent>());
        }
        eventMediators[key].UnSubscribe(handler);
    }
    // public abstract void Invoke<TKey>(BaseEvent @event) where TKey : TBaseKey;
    public virtual void Execute<TKey>(TKey key, TEvent @event) where TKey : TBaseKey
    {
        if (!eventMediators.ContainsKey(key))
            eventMediators.Add(key, new EventMediator<TEvent>());

        eventMediators[key].Execute(@event);
    }
}
