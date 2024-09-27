using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMediator<TEvent> : BaseEventMediator<TEvent> where TEvent : BaseEvent
{
    public Action<TEvent> eventAction;

    public override void Execute(TEvent @event)
    {
        eventAction?.Invoke(@event);
        @event.onAfterExcute?.Invoke(@event.ExcuteEvent());
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
