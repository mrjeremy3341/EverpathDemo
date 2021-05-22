using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UnitAbilities : MonoBehaviour
{
    public Ability selectedAbility; // TODO: This should be readOnly and selected via UI
    public BattleActions battleActions;
    public Ability[] unitAbilities;
    public GameObject actionsHolder;

    [Range(1, 3)]
    public int level = 1;

    bool requiresTarget;
    bool onlyTargetDamaged = false;
    TargetType targetType;    
    int range;

    public GameObject targettingController;

    private void Start()
    {
        //LoadAbilities();        
        battleActions = GetComponentInChildren<BattleActions>();
        selectedAbility.battleUnit = gameObject.GetComponent<BattleUnit>();
    }

    public void OnSelect()
    {
        //selectedAbility = whichever ability selected from ability menu list by player;
        //play ability preparation animations etc. while selection
        InitializeBaseParameters();
    }

    public void UseAbility(GridCell targetCell)
    {
        if (!selectedAbility.isSpecializedAbility)
        {
            for (int i = 0; i < selectedAbility.abilityBehaviors.Count; i++)
            {
                //Instantiate Ability
                var abilityinstance = Instantiate(selectedAbility.abilityBehaviors[i], actionsHolder.transform);
                //Get Targets from corresponding list
                var targets = GetTargets(selectedAbility.targetBehaviours[i], targetCell);
                //Parameterize
                ParameterizeEffect(abilityinstance.effect, abilityinstance, i);
                //Execute
                battleActions.battleUnit.battleManager.gridManager.ClearCells();
                foreach (var target in targets)
                {
                    abilityinstance.Execute(targetCell);
                    Debug.Log("Execution");
                }
            }
        }
        else
        {
            // Either run the code through the inherited scriptableObject
            selectedAbility.battleUnit = this.gameObject.GetComponent<BattleUnit>();
            // or have just have an inherited class
        }
    }

    private void ParameterizeEffect(AbilityEffects _effect, AbilityBehavior _abilityInstance, int i)
    {
        switch (_effect)
        {
            case AbilityEffects.None:
                break;
            case AbilityEffects.HealEffect:
                var healScript = _abilityInstance.GetComponent<HealBehaviour>();
                healScript.amount = selectedAbility.healAmount;
                break;
            case AbilityEffects.DamageEffect:
                var damageScript = _abilityInstance.GetComponent<DamageBehaviour>();
                damageScript.amount = selectedAbility.rawDamage;
                damageScript.isMagic = selectedAbility.isMagic;
                break;
            case AbilityEffects.MoveEffect:
                break;
            case AbilityEffects.PushEffect:
                break;
            case AbilityEffects.DamageOverTimeEffect:
                break;
            case AbilityEffects.ApplyCondition:
                var applyScript = _abilityInstance.GetComponent<ApplyCondition>();
                var condition = selectedAbility.conditions[i];      // Get corresponding condition to apply
                ParameterizeCondition(condition, applyScript);
                break;
            case AbilityEffects.SpawnPrefab:
                break;
            default:
                break;
        }        
    }

    private void ParameterizeRange()
    {

    }

    private void ParameterizeCondition(Conditions condition, ApplyCondition applyScript)
    {
        switch (condition)
        {
            case Conditions.None:
                break;
            case Conditions.Stunned:
                applyScript.duration = selectedAbility.stunnedDuration;
                if (selectedAbility.stunDelay == 0)
                {
                    applyScript.isInstant = true;
                }                
                break;
            case Conditions.Slowed:
                break;
            case Conditions.Sleep:
                break;
            case Conditions.Invisible:
                break;
            case Conditions.Taunted:
                break;
            case Conditions.Silenced:
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
            UseAbility(battleActions.battleUnit.currentCell);
        }
    }



    private void ExecuteBehaviours(GridCell[] targets)
    {

    }
}
