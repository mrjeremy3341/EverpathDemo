using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GridManager gridManager;
    public TurnManager turnManager;
    public UIManager uiManager;

    public List<BattleUnit> playerUnits;
    public List<BattleUnit> enemyUnits;
    public List<BattleUnit> spawnedUnits = new List<BattleUnit>();

    public GameObject targettingControllerPrefab;

    public void SpawnPlayers(List<GridCell> spawnCells)
    {
        foreach (BattleUnit player in playerUnits)
        {
            BattleUnit unit = Instantiate<BattleUnit>(player);
            GridCell cell = spawnCells[Random.Range(0, spawnCells.Count)];
            //////////
            UnitInfoSO _unitInfo = ScriptableObject.CreateInstance<UnitInfoSO>();
            unit.unitInfo = _unitInfo;
            UnitConditionsSO _unitConditions = ScriptableObject.CreateInstance<UnitConditionsSO>();
            unit.unitConditions = _unitConditions;
            var unitAbilities = unit.gameObject.GetComponent<UnitAbilities>();
            var targettingController = Instantiate(targettingControllerPrefab, unit.transform);
            unitAbilities.targettingController = targettingController;
            //////////
            spawnCells.Remove(cell);
            unit.SetUnitStart(cell);
            unit.battleManager = this;
            unit.isAlly = true;
            BattleCalculations.SetInitiative(unit);
            spawnedUnits.Add(unit);
        }
    }

    public void SpawnEnemies(List<GridCell> spawnCells)
    {
        foreach(BattleUnit enemy in enemyUnits)
        {
            BattleUnit unit = Instantiate<BattleUnit>(enemy);
            GridCell cell = spawnCells[Random.Range(0, spawnCells.Count)];
            UnitInfoSO _unitInfo = ScriptableObject.CreateInstance<UnitInfoSO>();
            unit.unitInfo = _unitInfo;
            UnitConditionsSO _unitConditions = ScriptableObject.CreateInstance<UnitConditionsSO>();
            unit.unitConditions = _unitConditions;
            spawnCells.Remove(cell);
            unit.SetUnitStart(cell);
            unit.battleManager = this;
            unit.isAlly = false;
            BattleCalculations.SetInitiative(unit);
            spawnedUnits.Add(unit);
        }
    }

    public void InitTurns()
    {
        turnManager.InitTurnOrder(spawnedUnits);
    }

    public void CheckForVictory()
    {
        List<BattleUnit> allyUnits = new List<BattleUnit>();
        List<BattleUnit> enemyUnits = new List<BattleUnit>();

        foreach (BattleUnit unit in spawnedUnits)
        {
            if (unit.isAlly)
            {
                allyUnits.Add(unit);
            }
            else
            {
                enemyUnits.Add(unit);
            }
        }

        bool isDead = true;
        foreach (BattleUnit unit in allyUnits)
        {
            if (!unit.isDead)
            {
                isDead = false;
            }
        }

        if (isDead)
        {
            uiManager.EndScreen(false);
        }

        isDead = true;
        foreach (BattleUnit unit in enemyUnits)
        {
            if (!unit.isDead)
            {
                isDead = false;
            }
        }

        if (isDead)
        {
            uiManager.EndScreen(true);
        }
    }
}
