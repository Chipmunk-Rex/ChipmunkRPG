using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMediatorContainerEnum<TEnum, TEvent> : EventMediatorContainer<TEnum, TEvent> where TEnum : Enum where TEvent : BaseEvent
{
}
