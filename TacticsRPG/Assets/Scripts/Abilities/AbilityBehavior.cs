using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum AbilityEffects
{
    None,
    HealEffect,
    DamageEffect,
    MoveEffect,
    PushEffect,
    DamageOverTimeEffect,
    ApplyCondition,
    SpawnPrefab
}

public abstract class AbilityBehavior : MonoBehaviour
{
    [ValidateInput("SendWarning", "Ensure AbilityBehavior object has a serialized Behaviour enum")]
    public AbilityEffects effect;
    public bool SendWarning()
    {
        if (effect == AbilityEffects.None)
        {
            return false;
        }
        return true;
    }

    public abstract void Execute(GridCell targetCell);
}
