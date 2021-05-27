using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOT : BaseCondition
{
    public int damage;

    public override BattleConditions battleConditions { get; set; }
    public override int duration { get; set; }

    public override void Effect()
    {
        BattleUnit battleUnit = battleConditions.battleUnit;
        battleUnit.TakeDamage(damage);
    }

    public override void End()
    {
        return;
    }
}
