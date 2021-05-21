using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUnit : MonoBehaviour
{
    public enum ActionMode
    {
        Idle, Move, Attack, Ability
    }

    public GridCell currentCell;
    public bool isAlly;
    public bool isTargetable = true;
    public BattleManager battleManager;
    public UnitStats unitStats;
    public BattleActions battleActions;
    public BattleConditions battleConditions;
    public ITurn battleTurn;
    public bool waitingForInput = false;
    public ActionMode actionMode = ActionMode.Idle;

    public UnitInfoSO unitInfo;
    public UnitConditionsSO unitConditions;

    public bool actionUsed = false;
    public bool moveUsed = false;

    public bool isDead = false;

    private void Awake()
    {
        battleTurn = GetComponent<ITurn>();
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
        this.transform.position = new Vector2(startCell.transform.position.x, startCell.transform.position.y + ((startCell.elevation -1) * .12f));
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
            StartCoroutine(LerpPosition(new Vector2(c.transform.position.x, c.transform.position.y + ((c.elevation - 1) * .12f)), .5f)); // TODO: I think there is a better way to account fo elevation height but thsi wroks for now
            yield return new WaitForSeconds(.5f);
        }
        
        this.currentCell = newCell;
        this.currentCell.currentUnit = this;
        moveUsed = true;
        battleManager.gridManager.ClearCells();
    }

    IEnumerator LerpPosition(Vector2 targetPosition, float duration)
    {
        float time = 0;
        Vector2 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }

    public void DamageUnit(int damage)
    {
        unitStats.currentHP -= damage;
        unitInfo.currentHP -= damage;
        battleManager.uiManager.SpawnDamageCounter(this, damage);

        if(unitStats.currentHP < 1)
        {
            isDead = true;
            Destroy(this.gameObject);
            battleManager.CheckForVictory();
        }
    }

    void HealUnit()
    {

    }

    void GetInititive()
    {

    }
}
