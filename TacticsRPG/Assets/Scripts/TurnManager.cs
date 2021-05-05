using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public Queue<BattleUnit> turnOrder = new Queue<BattleUnit>();
    public BattleUnit currentTurn;

    public void NextTurn()
    {
        currentTurn.battleConditions.CheckTimer();
        currentTurn.unitStats.currentAP += 1;
        if(currentTurn.unitStats.currentAP > currentTurn.unitStats.maxAP)
        {
            currentTurn.unitStats.currentAP = currentTurn.unitStats.maxAP;
        }

        if (!currentTurn.isDead)
        {
            turnOrder.Enqueue(currentTurn);
        }
        currentTurn = turnOrder.Dequeue();
        
        if (!currentTurn.isDead)
        {
            currentTurn.battleConditions.ExecuteEffects();
            currentTurn.battleTurn.TakeTurn();
        }
        else
        {
            NextTurn();
        }
        
    }

    public void InitTurnOrder(List<BattleUnit> allUnits)
    {
        List<BattleUnit> units = allUnits.OrderBy(e => e.unitStats.initiative).ToList<BattleUnit>();
        units.Reverse();

        foreach (BattleUnit unit in units)
        {
            turnOrder.Enqueue(unit);
        }

        currentTurn = turnOrder.Dequeue();
        currentTurn.battleConditions.ExecuteEffects();
        currentTurn.battleTurn.TakeTurn();
    }
}
