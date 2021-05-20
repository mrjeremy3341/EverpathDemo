using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBehaviour : AbilityBehavior
{
    public int ammount;
    public GridCell[] targets;
    public TargetBehaviour behaviourType;

    public override void Execute(GridCell targetCell)
    {
        targets = behaviourType.GetTargets(targetCell);

        foreach (GridCell target in targets)
        {
            BattleUnit unit = GetComponentInParent<BaseAbility>().battleActions.battleUnit;
            target.currentUnit.DamageUnit(-BattleCalculations.HealCalculation(unit, ammount));
        }
    }
}
