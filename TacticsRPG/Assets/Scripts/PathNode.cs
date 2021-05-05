using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public GridCell gridCell;
    public Vector2Int position;

    public bool visited = false;

    public int gCost;
    public int hCost;
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public PathNode previousNode = null;
}
