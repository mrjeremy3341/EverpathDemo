using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

public class BattleUnit : MonoBehaviour
{
    public enum ActionMode
    {
        Idle, Move, Attack, Ability
    }

    public GridCell currentCell;
    [ReadOnly]
    public GridCell moveNextCell;
    public bool isAlly;
    public bool isTargetable = true;
    public BattleManager battleManager;
    public UnitStats unitStats;
    public BattleActions battleActions;
    public BattleConditions battleConditions;
    public ITurn battleTurn;
    public bool waitingForInput = false;
    public ActionMode actionMode = ActionMode.Idle;
    public UnitConditionsSO unitConditions;
    public UnitInventory unitInventory;
    public UnitAnimations unitAnimation;    // assigned by UnitAnimation due to script execution
    CellDirection cellDir;

    public bool actionUsed = false;
    public bool moveUsed = false;

    public bool isDead = false;

    private void Awake()
    {
        battleTurn = GetComponent<ITurn>();
        battleConditions = GetComponentInChildren<BattleConditions>();
        battleConditions.battleUnit = this;     
    }


    private void Update()
    {
        if(actionUsed && moveUsed)
        {
            battleTurn.EndTurn();
        }
    }

    public void SetUnitStart(GridCell startCell)
    {
        this.currentCell = startCell;
        this.currentCell.currentUnit = this;
        this.transform.position = startCell.GetTargetPosition();
    }

    public IEnumerator MoveUnit(GridCell newCell)
    {
        // TODO: Change to move through the path eventually -- prolly switch to using dotween pro i dont like this current implementation
        this.currentCell.currentUnit = null;
        List<GridCell> path = AStar.FindPath(currentCell, newCell);

        

        foreach (GridCell c in path)
        {
            c.spriteRenderer.color = Color.green;
        }

        foreach (GridCell c in path)
        {
            int directionNo = Array.IndexOf(currentCell.neighbors, c);
            cellDir = (CellDirection)directionNo;
            unitAnimation.WalkSwitch(cellDir);

            StartCoroutine(LerpPosition(c.GetTargetPosition(), .5f));
            yield return new WaitForSeconds(.5f);

            currentCell = c;
        }

        unitAnimation.SetIdle(cellDir);

        this.currentCell = newCell;
        this.currentCell.currentUnit = this;
        moveUsed = true;
        battleManager.gridManager.ClearCells();
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    public void TakeDamage(int damage)
    {
        unitStats.currentHP -= damage;
        battleManager.uiManager.SpawnDamageCounter(this, damage);

        if(unitStats.currentHP < 1)
        {
            isDead = true;
            Destroy(this.gameObject);
            battleManager.CheckForVictory();
        }
    }

    public void ShowBasicAttackRange()
    {
        List<GridCell> cellsInRange = AStar.FindAttackRange(battleActions.battleUnit.currentCell, unitStats.basicAttackRange);
        foreach (GridCell cell in cellsInRange)
        {
            if (cell.currentUnit != null && cell.currentUnit.isTargetable)
            {
                if (battleActions.battleUnit.isAlly && !cell.currentUnit.isAlly)
                {
                    cell.selectable = true;
                }
            }

            cell.spriteRenderer.color = Color.red;
        }
    }

    public void BasicAttack(BattleUnit target)
    {
        int damage = BattleCalculations.BasicAttackDamage(battleActions.battleUnit, target, unitStats.isMagic);
        target.TakeDamage(damage);
        battleActions.battleUnit.battleManager.gridManager.ClearCells();
    }
}
