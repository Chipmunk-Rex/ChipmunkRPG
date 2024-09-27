using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEvent
{
    public Action onBeforeExcute;
    public Action onAfterExcute;
    public abstract void ExcuteEvent();
}
