using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : BaseCondition
{
    public override BattleConditions battleConditions { get; set; }
    public override int duration { get; set; }

    public override void Effect()
    {
        BattleUnit battleUnit = battleConditions.battleUnit;
        battleUnit.isTargetable = false;
        SpriteRenderer sr = battleUnit.GetComponentInChildren<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, .65f);
    }

    public override void End()
    {
        BattleUnit battleUnit = battleConditions.battleUnit;
        SpriteRenderer sr = battleUnit.GetComponentInChildren<SpriteRenderer>();
        sr.color = new Color(1, 1, 1, 1);
        battleUnit.isTargetable = true;
    }
}
