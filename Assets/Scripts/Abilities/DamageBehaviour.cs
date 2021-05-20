using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehaviour : AbilityBehavior
{
    public int ammount;
    public bool isMagic;
    public GridCell[] targets;
    public TargetBehaviour behaviourType;

    public override void Execute(GridCell targetCell)
    {
        targets = behaviourType.GetTargets(targetCell);

        foreach(GridCell target in targets)
        {
            BattleUnit unit = GetComponentInParent<BaseAbility>().battleActions.battleUnit;
            target.currentUnit.DamageUnit(BattleCalculations.DamageCalculation(unit, target.currentUnit, ammount, isMagic));
        }
    }
}
