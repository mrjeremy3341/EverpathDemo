using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehaviour : AbilityBehavior
{
    public int amount;
    public bool isMagic;
    public GridCell[] targets;

    public override void Execute(GridCell targetCell)
    {
        BattleUnit unit = GetComponentInParent<BattleUnit>();
        Debug.Log(targetCell.currentUnit.name);
        targetCell.currentUnit.DamageUnit(BattleCalculations.DamageCalculation(unit, targetCell.currentUnit, amount, isMagic));
    }
}
