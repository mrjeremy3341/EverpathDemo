using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class AbilityBehavior : MonoBehaviour
{
    public AbilityEffects effect;
    public BattleUnit currentUnit;

    public bool SendWarning()
    {
        if (effect == AbilityEffects.None)
        {
            return false;
        }
        return true;
    }

    public abstract void Execute(GridCell targetCell, BattleUnit unit);

}
