using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum EnumEntityState
{
    Idle, Move, Attack, SwimIdle, SwimMove, Use, Died, Build, Eat, Die
}
