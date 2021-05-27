using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public BattleActions battleActions;

    public int attackRange;
    public bool isMagic;

    public void ShowRange()
    {
        List<GridCell> cellsInRange = AStar.FindAttackRange(battleActions.battleUnit.currentCell, attackRange);
        foreach (GridCell cell in cellsInRange)
        {
            if(cell.currentUnit != null && cell.currentUnit.isTargetable)
            {
                if (battleActions.battleUnit.isAlly && !cell.currentUnit.isAlly)
                {
                    cell.selectable = true;
                }
            }

            cell.spriteRenderer.color = Color.red;
        }
    }

    public void DamageTarget(BattleUnit target)
    {
        int damage = BattleCalculations.BasicAttackDamage(battleActions.battleUnit, target, isMagic);
        target.DamageUnit(damage);
        battleActions.battleUnit.battleManager.gridManager.ClearCells();
    }
}
