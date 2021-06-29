using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : BaseCondition
{
    public string taunter;
    private BattleUnit taunterUnit;
    public List<BattleUnit> targetsToFlip = new List<BattleUnit>();

    public override BattleConditions battleConditions { get; set; }
    public override int duration { get; set; }

    private void Awake()
    {
        GameObject gameObject = GameObject.Find(taunter + "(Clone)");
        taunterUnit = gameObject.GetComponent<BattleUnit>();
        Debug.Log(taunterUnit);
    }

    public override void Effect()
    {
        BattleUnit battleUnit = battleConditions.battleUnit;
        foreach(BattleUnit unit in battleUnit.battleManager.turnManager.turnOrder)
        {
            if(unit != taunterUnit)
            {
                if (taunterUnit.isAlly)
                {
                    if (unit.isTargetable && !unit.isAlly)
                    {
                        unit.isTargetable = false;
                        targetsToFlip.Add(unit);
                    }
                }
                else
                {
                    if (unit.isTargetable && unit.isAlly)
                    {
                        unit.isTargetable = false;
                        targetsToFlip.Add(unit);
                    }
                }
            }
        }
    }

    public override void End()
    {
        foreach(BattleUnit unit in targetsToFlip)
        {
            unit.isTargetable = true;
        }
    }
}
