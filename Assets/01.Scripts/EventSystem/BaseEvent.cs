using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEvent
{
    public Action<EnumEventResult> onAfterExcute;
    public abstract EnumEventResult ExcuteEvent();
}
