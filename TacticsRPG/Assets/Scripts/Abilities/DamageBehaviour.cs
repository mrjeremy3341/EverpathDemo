using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehaviour : AbilityBehavior
{
    public int amount;
    public bool isMagic;
    public GridCell[] targets;

    public override void Execute(GridCell targetCell, BattleUnit unit)
    {
        targetCell.currentUnit.TakeDamage(BattleCalculations.DamageCalculation(unit, targetCell.currentUnit, amount, isMagic));
    }
}
