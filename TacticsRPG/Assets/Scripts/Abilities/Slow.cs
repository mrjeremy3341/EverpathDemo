using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : BaseCondition
{
    public int slowPercent;

    public override BattleConditions battleConditions { get; set; }
    public override int duration { get; set; }

    public override void Effect()
    {
        BattleUnit battleUnit = battleConditions.battleUnit;
        int slowMod = 100 - slowPercent;
        int currentMovement = battleUnit.unitStats.currentMP;

        int newMovement = Mathf.RoundToInt((currentMovement * slowMod) / 100);
        battleUnit.unitStats.currentMP = newMovement;
    }

    public override void End()
    {
        return;
    }
}
