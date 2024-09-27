using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMediator<TEvent> : BaseEventMediator<TEvent> where TEvent : BaseEvent
{
    public Action<TEvent> eventAction;

    public override void Invoke(TEvent @event)
    {
        eventAction?.Invoke(@event);
        @event.onBeforeExcute?.Invoke();
        @event.ExcuteEvent();
        @event.onAfterExcute?.Invoke();
    }

    public override void Subscribe(Action<TEvent> handler)
    {
        eventAction += handler;
    }
    public override void UnSubscribe(Action<TEvent> handler)
    {
        eventAction -= handler;
    }
}
