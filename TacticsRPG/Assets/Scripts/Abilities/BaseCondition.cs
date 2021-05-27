using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCondition : MonoBehaviour
{
    public abstract BattleConditions battleConditions { get; set; }
    public abstract int duration { get; set; }
    public abstract void Effect();
    public abstract void End();
}
