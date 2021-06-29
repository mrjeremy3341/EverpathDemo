using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UnitAbilities : MonoBehaviour
{
    public Ability selectedAbility;         // TODO: This should be readOnly and selected via UI
    public BattleActions battleActions;
    public List<Ability> unitAbilities;
    public GameObject actionsHolder;
    public GameObject abilityManager;       // reference injected from BattleManager upon unitcreation;
    public GameObject targettingController; // reference injected from BattleManager upon unitcreation;

    [Range(1, 3)]
    public int level = 1;

    bool requiresTarget;
    bool onlyTargetDamaged = false;
    TargetType targetType;    
    public int range;

    private void Start()
    {
        //LoadAbilities();        
        battleActions = GetComponentInChildren<BattleActions>();
        if (selectedAbility != null)
        {
            selectedAbility.battleUnit = gameObject.GetComponent<BattleUnit>();
        }        
    }

    public void OnSelect()
    {
        //selectedAbility = whichever ability selected from ability menu list by player;
        //play ability preparation animations etc. while selection
        InitializeBaseParameters();
    }

    public void InitializeAbility(GridCell targetCell)
    {
        for (int i = 0; i < selectedAbility.abilityEffects.Count; i++)
        {
            battleActions.battleUnit.battleManager.gridManager.ClearCells();
            var targets = GetTargets(selectedAbility.targetBehaviours[i], targetCell);
            foreach (var target in targets)
            {
                ExecuteEffect(selectedAbility.abilityEffects[i], target);
                target.currentUnit.unitConditions.AttemptConditionApply(selectedAbility, selectedAbility.conditions[i]);
            }
        }

        if (selectedAbility.hasStackingAbilities)
        {
            foreach (var stackedAbility in selectedAbility.stackingAbilities)
            {
                for (int i = 0; i < stackedAbility.abilityEffects.Count; i++)
                {
                    battleActions.battleUnit.battleManager.gridManager.ClearCells();
                    var targets = GetTargets(stackedAbility.targetBehaviours[i], targetCell);
                    foreach (var target in targets)
                    {
                        ExecuteEffect(stackedAbility.abilityEffects[i], target);
                        target.currentUnit.unitConditions.AttemptConditionApply(stackedAbility, stackedAbility.conditions[i]);
                    }
                }
            }
        }
    }

    private void ExecuteEffect(AbilityEffects _effect, GridCell targetCell)
    {
        switch (_effect)
        {
            case AbilityEffects.None:
                break;

            case AbilityEffects.HealEffect:
                var healScript = abilityManager.GetComponent<HealBehaviour>();
                healScript.amount = selectedAbility.healAmount;
                healScript.Execute(targetCell, battleActions.battleUnit);
                break;

            case AbilityEffects.DamageEffect:
                var damageScript = abilityManager.GetComponent<DamageBehaviour>();
                damageScript.amount = selectedAbility.rawDamage;
                damageScript.isMagic = selectedAbility.isMagic;
                damageScript.Execute(targetCell, battleActions.battleUnit);
                break;

            case AbilityEffects.MoveEffect:
                var moveScript = abilityManager.GetComponent<MoveAttack>();
                moveScript.Execute(targetCell, battleActions.battleUnit);
                break;

            case AbilityEffects.PushEffect:
                break;
            case AbilityEffects.ApplyCondition:
                break;

            case AbilityEffects.SpawnPrefab:
                break;
            default:
                break;
        }        
    }

    private GridCell[] GetTargets(TargetBehaviors _targetBehaviour, GridCell targetCell)
    {
        GridCell[] cells = null;

        if (!selectedAbility.multipleTargetBehaviours)
        {
            switch (_targetBehaviour)
            {
                case TargetBehaviors.SingleTarget:
                    cells = targettingController.GetComponent<SingleTarget>().GetTargets(targetCell);
                    break;

                case TargetBehaviors.SelfTarget:
                    cells = targettingController.GetComponent<SelfTarget>().GetTargets(targetCell);
                    break;

                case TargetBehaviors.areaOfEffect:
                    var aOE = targettingController.GetComponent<AOE>();
                    aOE.targetType = selectedAbility.baseTargetType;
                    aOE.range = selectedAbility.targettingRange;
                    cells = targettingController.GetComponent<AOE>().GetTargets(targetCell);
                    break;

                case TargetBehaviors.GridCell:
                    // no script                    
                    cells[0] = targetCell;

                    break;

                default:
                    break;
            }

            return cells;
        }

        return null;
    }

    public void InitializeBaseParameters()      // PURELY FOR DEMONSTRATION PURPROSES. Technically would work through a UI selectable
    {
        requiresTarget = selectedAbility.requiresTarget;
        onlyTargetDamaged = selectedAbility.onlyTargetDamaged;
        targetType = selectedAbility.baseTargetType;
        range = selectedAbility.targettingRange;
    }

    public void ShowRange()
    {
        InitializeBaseParameters();

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
            InitializeAbility(battleActions.battleUnit.currentCell);
        }
    }

    public void Execute(GridCell targetCell)
    {

    }
}
