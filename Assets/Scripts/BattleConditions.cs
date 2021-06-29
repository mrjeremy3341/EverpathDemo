using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Conditions
{
    None,
    Stunned,
    Slowed,
    Sleep,
    Invisible,
    Taunted,
    Silenced,
    DamageOverTime
}

public class BattleConditions : MonoBehaviour
{
    public BattleUnit battleUnit;
    public UnitConditionsSO unitConditions;

    private void Start()
    {
        unitConditions = battleUnit.unitConditions;        
    }

    public bool CheckCondition(Conditions condition)
    {
        if (condition == Conditions.None) { return false; }
        if (condition == Conditions.Stunned && battleUnit.unitConditions.stunned) { return true; }
        if (condition == Conditions.Slowed && battleUnit.unitConditions.slowed) { return true; }
        if (condition == Conditions.Sleep && battleUnit.unitConditions.sleep) { return true; }
        if (condition == Conditions.Invisible && battleUnit.unitConditions.invisible) { return true; }
        if (condition == Conditions.Taunted && battleUnit.unitConditions.taunted) { return true; }
        if (condition == Conditions.Silenced && battleUnit.unitConditions.silenced) { return true; }
        if (condition == Conditions.DamageOverTime && battleUnit.unitConditions.dOT) { return true; }

        return false;
    }

    public void ExecuteEffects()
    {
        DOT();
    }

    private void DOT()
    {
        if (battleUnit.battleConditions.CheckCondition(Conditions.DamageOverTime))
        {
            battleUnit.TakeDamage(unitConditions.dOTDamage);
        }
    }

    public void ClickTimer()
    {
        unitConditions = battleUnit.unitConditions;
        unitConditions.DropDuration();
    }
}
