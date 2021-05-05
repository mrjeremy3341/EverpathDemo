using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfTarget : TargetBehaviour
{
    public override GridCell[] GetTargets(GridCell targetCell)
    {
        GridCell[] target = new GridCell[1];
        target[0] = GetComponentInParent<BattleActions>().battleUnit.currentCell;

        return target;


    }
}
