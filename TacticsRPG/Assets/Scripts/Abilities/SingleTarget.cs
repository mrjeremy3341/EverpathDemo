using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTarget : TargetBehaviour
{
    public override GridCell[] GetTargets(GridCell targetCell)
    {
        GridCell[] target = new GridCell[1];
        target[0] = targetCell;

        return target;
    }
}
