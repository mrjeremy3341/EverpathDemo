using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAI : MonoBehaviour, ITurn
{
    public struct AIaction
    {
        public BattleUnit targetUnit;
        public GridCell targetCell;

        public AIaction(BattleUnit unit, GridCell cell)
        {
            this.targetUnit = unit;
            this.targetCell = cell;
        }
    }

    public BattleUnit battleUnit;

    public int aiLife;
    public int aiPower;
    public int playerLife;
    public int playerPower;

    public AIaction desiredAction;
    public List<AIaction> possibleActions = new List<AIaction>();

    private void Awake()
    {
        battleUnit = GetComponent<BattleUnit>();
    }

    public void TakeTurn()
    {
        ResetGameState();
        SetGameState();
        FindPossibleActions();

        if (CheckActions())
        {
            desiredAction = SimulateActions();
        }
        else
        {
            desiredAction = CloseDistance();
        }

        
        Debug.Log(desiredAction.targetCell + ", " + desiredAction.targetUnit);
        StartCoroutine(ExecuteTurn());
    }

    private AIaction SimulateActions()
    {
        int[] actionScore = new int[possibleActions.Count];
        int actionIndex = 0;
        for (int i = 0; i < actionScore.Length; i++)
        {
            actionScore[i] = GetScore(possibleActions[i]);
            if (actionScore[i] > actionScore[actionIndex])
            {
                actionIndex = i;
            }
        }

        return possibleActions[actionIndex];
    }

    private int GetScore(AIaction action)
    {
        int newAiLife = 0;
        int newAiPower = 0;
        int newPlayerLife = 0;
        int newPlayerPower = 0;
        int attackDamage = 0;

        if(action.targetUnit != null)
        {
            attackDamage = BattleCalculations.BasicAttackDamage(battleUnit, action.targetUnit, battleUnit.battleActions.basicAttack.isMagic);
        }

        foreach(BattleUnit unit in battleUnit.battleManager.turnManager.turnOrder)
        {
            if (unit.isAlly)
            {
                newPlayerLife += unit.unitStats.currentHP;
                newPlayerPower += GetPower(unit, true);

                if (action.targetUnit == unit)
                {
                    newPlayerLife -= attackDamage;
                }
            }
            else
            {
                newAiLife += unit.unitStats.currentHP;
                newAiPower += GetPower(unit, false);
            }
        }
        newAiLife += battleUnit.unitStats.currentHP;
        newAiPower += GetPower(battleUnit, false);

        int aiLifeDiff = aiLife - newAiLife;
        int aiPowerDiff = aiPower - newAiPower;
        int playerLifeDiff = playerLife - newPlayerLife;
        int playerPowerDiff = playerPower - newPlayerPower;

        return (playerLifeDiff + playerPowerDiff) - (aiLifeDiff - aiPowerDiff);
    }

    private void FindPossibleActions()
    {
        foreach(GridCell cell in FindPossibleMoves())
        {
            List<BattleUnit> possibleAttacks = FindPossibleAttacks(cell);
            if(possibleAttacks.Count > 0)
            {
                foreach(BattleUnit unit in possibleAttacks)
                {
                    AIaction action = new AIaction(unit, cell);
                    possibleActions.Add(action);
                }
            }
            else
            {
                AIaction action = new AIaction(null, cell); ;
                possibleActions.Add(action);
            }
        }
    }

    private List<BattleUnit> FindPossibleAttacks(GridCell currentCell)
    {
        List<BattleUnit> possibleTargets = new List<BattleUnit>();
        foreach(GridCell cell in AStar.FindAttackRange(currentCell, battleUnit.battleActions.basicAttack.attackRange))
        {
            if(cell.currentUnit != null && cell.currentUnit.isAlly)
            {
                possibleTargets.Add(cell.currentUnit);
            }
        }

        return possibleTargets;
    }

    private List<GridCell> FindPossibleMoves()
    {
        List<GridCell> cellInRange = AStar.FindMovementRange(battleUnit.currentCell, battleUnit.unitStats.currentMP);
        List<GridCell> moveCells = new List<GridCell>();
        foreach(GridCell cell in cellInRange)
        {
            if(cell.currentUnit == null)
            {
                moveCells.Add(cell);
                
            }
        }
        moveCells.Add(battleUnit.currentCell);

        return moveCells;
    }

    private bool CheckActions()
    {
        bool attack = false;
        foreach (AIaction action in possibleActions)
        {
            if (action.targetUnit != null)
            {
                attack = true;
            }
        }

        return attack;
    }

    private AIaction CloseDistance()
    {
        int finalDistance = 100;
        BattleUnit targetUnit = null;
        foreach(BattleUnit unit in battleUnit.battleManager.turnManager.turnOrder)
        {
            if (unit.isAlly)
            {
                int distance = AStar.FindPath(battleUnit.currentCell, unit.currentCell).Count;
                if (distance < finalDistance)
                {
                    finalDistance = distance;
                    targetUnit = unit;
                }
            }
        }

        GridCell targetCell = null;
        List<GridCell> cellsInRange = AStar.FindMovementRange(battleUnit.currentCell, battleUnit.unitStats.currentMP);
        List<GridCell> path = AStar.FindPath(battleUnit.currentCell, targetUnit.currentCell);
        foreach (GridCell cell in cellsInRange)
        {
            if (path.Contains(cell))
            {
                targetCell = cell;
            }
        }

        if(targetCell.currentUnit != null)
        {
            foreach(GridCell cell in targetCell.neighbors)
            {
                if (cellsInRange.Contains(cell) && cell.currentUnit == null)
                {
                    targetCell = cell;
                    break;
                }
            }
        }

        return new AIaction(null, targetCell);
    }

    public IEnumerator ExecuteTurn()
    {
        if(desiredAction.targetUnit != null)
        {
            if (InStartRange())
            {
                if(battleUnit.unitStats.currentAP == battleUnit.unitStats.maxAP)
                {
                    battleUnit.battleActions.currentAbilites[0].ShowRange();
                    if (desiredAction.targetUnit.currentCell.selectable)
                    {
                        battleUnit.battleActions.currentAbilites[0].Execute(desiredAction.targetUnit.currentCell);
                        battleUnit.unitStats.currentAP = 0;
                        yield return new WaitForSeconds(.1f);
                        yield return StartCoroutine(battleUnit.MoveUnit(desiredAction.targetCell));
                    }
                    else
                    {
                        GridCell targetCell = null;
                        foreach(GridCell cell in AStar.FindAttackRange(battleUnit.currentCell, battleUnit.battleActions.currentAbilites[0].range))
                        {
                            if (cell.selectable)
                            {
                                targetCell = cell;
                                break;
                            }
                        }

                        if(targetCell != null)
                        {
                            battleUnit.battleActions.currentAbilites[0].Execute(targetCell);
                            battleUnit.unitStats.currentAP = 0;
                            yield return new WaitForSeconds(.1f);
                            yield return StartCoroutine(battleUnit.MoveUnit(desiredAction.targetCell));
                        }
                        else
                        {
                            desiredAction.targetUnit.DamageUnit(BattleCalculations.BasicAttackDamage(battleUnit, desiredAction.targetUnit, battleUnit.battleActions.basicAttack.isMagic));
                            yield return new WaitForSeconds(.1f);
                            yield return StartCoroutine(battleUnit.MoveUnit(desiredAction.targetCell));
                        }
                    }
                }
                else
                {
                    desiredAction.targetUnit.DamageUnit(BattleCalculations.BasicAttackDamage(battleUnit, desiredAction.targetUnit, battleUnit.battleActions.basicAttack.isMagic));
                    yield return new WaitForSeconds(.1f);
                    yield return StartCoroutine(battleUnit.MoveUnit(desiredAction.targetCell));
                }  
            }
            else
            {
                yield return StartCoroutine(battleUnit.MoveUnit(desiredAction.targetCell));
                if (battleUnit.unitStats.currentAP == battleUnit.unitStats.maxAP)
                {
                    battleUnit.battleActions.currentAbilites[0].ShowRange();
                    if (desiredAction.targetUnit.currentCell.selectable)
                    {
                        battleUnit.battleActions.currentAbilites[0].Execute(desiredAction.targetUnit.currentCell);
                        battleUnit.unitStats.currentAP = 0;
                        yield return new WaitForSeconds(.1f);
                    }
                    else
                    {
                        GridCell targetCell = null;
                        foreach (GridCell cell in AStar.FindAttackRange(battleUnit.currentCell, battleUnit.battleActions.currentAbilites[0].range))
                        {
                            if (cell.selectable)
                            {
                                targetCell = cell;
                                break;
                            }
                        }

                        if (targetCell != null)
                        {
                            battleUnit.battleActions.currentAbilites[0].Execute(targetCell);
                            battleUnit.unitStats.currentAP = 0;
                            yield return new WaitForSeconds(.1f);
                        }
                        else
                        {
                            desiredAction.targetUnit.DamageUnit(BattleCalculations.BasicAttackDamage(battleUnit, desiredAction.targetUnit, battleUnit.battleActions.basicAttack.isMagic));
                            yield return new WaitForSeconds(.1f);
                        }
                    }
                }
                else
                {
                    desiredAction.targetUnit.DamageUnit(BattleCalculations.BasicAttackDamage(battleUnit, desiredAction.targetUnit, battleUnit.battleActions.basicAttack.isMagic));
                    yield return new WaitForSeconds(.1f);
                }
                
            }
        }
        else
        {
            yield return StartCoroutine(battleUnit.MoveUnit(desiredAction.targetCell));
        }

        yield return new WaitForSeconds(.25f);

        EndTurn();
    }

    public void SetGameState()
    {
        foreach(BattleUnit unit in battleUnit.battleManager.turnManager.turnOrder)
        {
            if (unit.isAlly)
            {
                playerLife += unit.unitStats.currentHP;
                playerPower += GetPower(unit, true);
            }
            else
            {
                aiLife += unit.unitStats.currentHP;
                aiPower += GetPower(unit, false);
            }
        }
        aiLife += battleUnit.unitStats.currentHP;
        aiPower += GetPower(battleUnit, false);
    }

    public int GetPower(BattleUnit unit, bool isAlly)
    {
        int power = 0;
        bool canAttack = false;

        foreach (GridCell cell in AStar.FindAttackRange(unit.currentCell, unit.battleActions.basicAttack.attackRange))
        {
            if (cell.currentUnit != null && cell.currentUnit.isAlly == !isAlly)
            {
                canAttack = true;
            }
        }
        if (canAttack)
        {
            if (unit.battleActions.basicAttack.isMagic)
            {
                power += unit.unitStats.magic;
            }
            else
            {
                power += unit.unitStats.attack;
            }
        }

        return power;
    }

    public bool InStartRange()
    {
        foreach(GridCell cell in AStar.FindAttackRange(battleUnit.currentCell, battleUnit.battleActions.basicAttack.attackRange))
        {
            if(cell.currentUnit == desiredAction.targetUnit)
            {
                return true;
            }
            else
            {
                continue;
            }
        }

        return false;
    }

    public void ResetGameState()
    {
        aiLife = 0;
        aiPower = 0;
        playerLife = 0;
        playerPower = 0;

        possibleActions.Clear();
        desiredAction.targetCell = null;
        desiredAction.targetUnit = null;
    }

    public void EndTurn()
    {
        battleUnit.unitStats.currentMP = battleUnit.unitStats.maxMP;
        battleUnit.battleManager.turnManager.NextTurn();
    }
}