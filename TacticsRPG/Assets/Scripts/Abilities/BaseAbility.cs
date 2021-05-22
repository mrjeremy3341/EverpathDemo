using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
[System.Flags]
public enum TargetType
{
    Ground = 1,
    Ally = 2,
    Enemy = 4
}
*/

public class BaseAbility : AbilityBehavior
{
    public Ability ability; // TODO: Make list
    public BattleActions battleActions;

    [Range(1,3)]
    public int level = 1;

    public bool requiresTarget;
    public bool onlyTargetDamaged = false;
    public TargetType targetType;
    public int range;

    public List<AbilityBehavior> abilityBehaviors;

    public void Init()  
    {
        requiresTarget = ability.requiresTarget;
        onlyTargetDamaged = ability.onlyTargetDamaged;
        targetType = ability.baseTargetType;
        range = ability.targettingRange;
    }

    public void ShowRange()
    {
        Init();

        if (requiresTarget)
        {
            List<GridCell> cellsInRange = AStar.FindAttackRange(battleActions.battleUnit.currentCell, range);
            foreach (GridCell cell in cellsInRange)
            {
                if (battleActions.battleUnit.isAlly)
                {
                    //Range Visuals
                    cell.spriteRenderer.color = Color.red;
                }

                if (cell.currentUnit != null && cell.currentUnit.isTargetable)
                {
                    if (onlyTargetDamaged)
                    {
                        if (cell.currentUnit.isAlly && targetType.HasFlag(TargetType.Ally) && cell.currentUnit.unitStats.currentHP < cell.currentUnit.unitStats.maxHP)
                        {
                            cell.selectable = true;
                        }
                        if (!cell.currentUnit.isAlly && targetType.HasFlag(TargetType.Enemy) && cell.currentUnit.unitStats.currentHP < cell.currentUnit.unitStats.maxHP)
                        {
                            cell.selectable = true;
                        }
                    }
                    else
                    {
                        if (cell.currentUnit.isAlly && targetType.HasFlag(TargetType.Ally))
                        {
                            cell.selectable = true;
                        }
                        if (!cell.currentUnit.isAlly && targetType.HasFlag(TargetType.Enemy))
                        {
                            cell.selectable = true;
                        }
                    } 
                }
                else if (targetType.HasFlag(TargetType.Ground))
                {
                    cell.selectable = true;
                }
            }
        }
        else
        {
            battleActions.battleUnit.actionMode = BattleUnit.ActionMode.Idle;
            battleActions.battleUnit.actionUsed = true;  
            Execute(battleActions.battleUnit.currentCell);
        }
    }

    public override void Execute(GridCell targetCell)
    {        
        battleActions.battleUnit.battleManager.gridManager.ClearCells();
        foreach(AbilityBehavior behavior in abilityBehaviors)
        {
            behavior.Execute(targetCell);
        }
        
    }
}
