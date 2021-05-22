using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBehaviour : AbilityBehavior
{
    public int amount;
    public GridCell[] targets;

    public override void Execute(GridCell targetCell)
    {
        BattleUnit unit = GetComponentInParent<BaseAbility>().battleActions.battleUnit;
        targetCell.currentUnit.DamageUnit(-BattleCalculations.HealCalculation(unit, amount));
    }
}
