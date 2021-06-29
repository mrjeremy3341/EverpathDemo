using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyCondition : AbilityBehavior
{
    public Ability ability;
    public int duration;
    public int damageTick;
    public bool isInstant;
    public Conditions condition;
    public GridCell[] targets;
    public TargetBehaviour behaviourType;

    public override void Execute(GridCell targetCell, BattleUnit unit)
    {
        // Basically null - this script is a just a placeholder for when an ability simply sets a condition. The condition is set in UseAbility

        /*
        Debug.Log("Condition Execute");
        targetCell.currentUnit.unitConditions.AttemptConditionApply(ability, condition);
        */
    }
}
