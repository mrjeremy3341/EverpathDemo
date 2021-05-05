using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE : TargetBehaviour
{
    public int range;
    public TargetType targetType;

    public override GridCell[] GetTargets(GridCell targetCell)
    {
        List<GridCell> targetCells = new List<GridCell>();

        GridCell[] cellsInRange = AStar.FindAttackRange(targetCell, range).ToArray();
        foreach(GridCell cell in cellsInRange)
        {
            if(cell.currentUnit != null)
            {
                if (cell.currentUnit != null)
                {
                    if (cell.currentUnit.isAlly && targetType.HasFlag(TargetType.Ally))
                    {
                        targetCells.Add(cell);
                    }
                    if (!cell.currentUnit.isAlly && targetType.HasFlag(TargetType.Enemy))
                    {
                        targetCells.Add(cell);
                    }
                }
                else if (targetType.HasFlag(TargetType.Ground))
                {
                    targetCells.Add(cell);
                }
            }
        }

        return targetCells.ToArray();
    }
}
