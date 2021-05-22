using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyCondition : AbilityBehavior
{
    public Ability ability;
    public int duration;
    public bool isInstant;
    public Conditions condition;
    public GridCell[] targets;
    public TargetBehaviour behaviourType;

    public override void Execute(GridCell targetCell)
    {
        targetCell.currentUnit.unitConditions.AttemptConditionApply(ability, condition);

        /*
        BaseCondition newCondition = Instantiate<BaseCondition>(condition);
        newCondition.transform.SetParent(targetCell.currentUnit.battleConditions.transform);
        targetCell.currentUnit.battleConditions.conditions.Add(newCondition);
        newCondition.duration = duration;
        newCondition.battleConditions = targetCell.currentUnit.battleConditions;
        if (isInstant)
        {
            newCondition.Effect();
        }
        */
    }
}
