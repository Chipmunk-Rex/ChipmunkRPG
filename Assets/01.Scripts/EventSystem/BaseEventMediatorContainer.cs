using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEventMediatorContainer<TBaseKey>
{
    public Dictionary<TBaseKey, EventMediator<BaseEvent>> eventMediators = new();

    // public abstract void Subscribe<TKey>(Action<BaseEvent> handler) where TKey : TBaseKey;
    public abstract void Subscribe<TKey>(TKey key, Action<BaseEvent> handler) where TKey : TBaseKey;
    // public abstract void UnSubscribe<TKey>(Action<BaseEvent> handler) where TKey : TBaseKey;
    public abstract void UnSubscribe<TKey>(TKey key, Action<BaseEvent> handler) where TKey : TBaseKey;
    // public abstract void Invoke<TKey>(BaseEvent @event) where TKey : TBaseKey;
    public abstract void Invoke<TKey>(TKey key, BaseEvent @event) where TKey : TBaseKey;
}
