using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAttack : AbilityBehavior
{
    public override void Execute(GridCell targetCell, BattleUnit unit)
    {
        unit.currentCell.currentUnit = null;
        unit.currentCell = targetCell;
        targetCell.currentUnit = unit;
        unit.transform.position = new Vector2(targetCell.transform.position.x, targetCell.transform.position.y + ((targetCell.elevation - 1) * .12f));

        unit.waitingForInput = true;
        unit.actionMode = BattleUnit.ActionMode.Attack;
        unit.ShowBasicAttackRange();
    }
}
