using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnInput : MonoBehaviour, ITurn
{
    public BattleUnit battleUnit;
    public bool turnActive = false;

    private void Awake()
    {
        battleUnit = GetComponent<BattleUnit>();
    }

    public void Update()
    {
        if(turnActive && battleUnit.waitingForInput)
        {
            GridCell targetCell = null;
            if (Input.GetMouseButtonDown(0))
            {
                targetCell = GetInput();
            }

            if (Input.GetMouseButtonDown(1))
            {
                battleUnit.actionMode = BattleUnit.ActionMode.Idle;
                battleUnit.battleManager.gridManager.ClearCells();
            }
            
            if(targetCell != null)
            {
                if(battleUnit.actionMode == BattleUnit.ActionMode.Move)
                {
                    battleUnit.actionMode = BattleUnit.ActionMode.Idle;
                    StartCoroutine(battleUnit.MoveUnit(targetCell));
                }
                else if(battleUnit.actionMode == BattleUnit.ActionMode.Attack)
                {
                    if(targetCell.currentUnit != null)
                    {
                        battleUnit.actionMode = BattleUnit.ActionMode.Idle;
                        battleUnit.battleActions.basicAttack.DamageTarget(targetCell.currentUnit);
                        battleUnit.actionUsed = true;
                    }
                }
                else if (battleUnit.actionMode == BattleUnit.ActionMode.Ability)
                {
                    battleUnit.actionMode = BattleUnit.ActionMode.Idle;
                    battleUnit.battleActions.currentAbilites[0].Execute(targetCell);
                    battleUnit.unitStats.currentAP = 0;
                    battleUnit.actionUsed = true;
                }
            }
        }
    }

    public void TakeTurn()
    {
        turnActive = true;
    }

    public void EndTurn()
    {
        battleUnit.unitStats.currentMP = battleUnit.unitStats.maxMP;
        turnActive = false;
        battleUnit.actionUsed = false;
        battleUnit.moveUsed = false;
        battleUnit.battleManager.turnManager.NextTurn();
    }

    public GridCell GetInput()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null )
        {
            GridCell cell = hit.collider.transform.GetComponent<GridCell>();
            if (cell.selectable)
            {
                return cell;
            }
        }

        return null;
    }
}
