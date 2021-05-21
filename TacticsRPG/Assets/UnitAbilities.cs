using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAbilities : MonoBehaviour
{
    public Ability ability; // TODO: Make list of abilities
    public BattleActions battleActions;

    [Range(1, 3)]
    public int level = 1;

    bool requiresTarget;
    bool onlyTargetDamaged = false;
    TargetType targetType;
    
    int range;

    [Header("Behaviours")]
    public HealBehaviour healBehaviour;
    public DamageBehaviour damageBehaviour;
    public MoveAttack moveAttack;

    [Header("Target Types")]
    public AOE aOE;
    public SelfTarget selfTarget;
    public SingleTarget singleTarget;

    public GameObject container;
    

    public void Init()      // PURELY FOR DEMONSTRATION PURPROSES. Technically would work through a UI selectable
    {
        requiresTarget = ability.requiresTarget;
        onlyTargetDamaged = ability.onlyTargetDamaged;
        targetType = ability.targetType;
        range = ability.range;
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

    public void Execute(GridCell targetCell)
    {
        battleActions.battleUnit.battleManager.gridManager.ClearCells();
        // MAYBE USE REFLECTION
                
        var targets = GetTargetType(targetCell);
        ExecuteBehaviours(targets);
    }

    private GridCell[] GetTargetType(GridCell targetCell)
    {
        GridCell[] cells  = null;

        if (!ability.multipleTargetBehaviours)
        {
            switch (ability.targetBehaviour)
            {
                case TargetBehaviors.SingleTarget:
                    cells = container.GetComponent<SingleTarget>().GetTargets(targetCell);
                    break;

                case TargetBehaviors.SelfTarget:
                    cells =  container.GetComponent<SelfTarget>().GetTargets(targetCell);
                    break;

                case TargetBehaviors.AoE:
                    cells = container.GetComponent<AOE>().GetTargets(targetCell);
                    break;

                case TargetBehaviors.GridCell:
                    
                    break;

                default:
                    break;
            }

            return cells;
        }

        return null;
    }

    private void ExecuteBehaviours(GridCell[] targets)
    {

    }
}
