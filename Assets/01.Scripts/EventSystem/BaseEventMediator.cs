using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEventMediator<TEvent> where TEvent : BaseEvent
{
    public abstract void Subscribe(Action<TEvent> handler);
    public abstract void UnSubscribe(Action<TEvent> handler);
    public abstract void Invoke(TEvent @event);
}
