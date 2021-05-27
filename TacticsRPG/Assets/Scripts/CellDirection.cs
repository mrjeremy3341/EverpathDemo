using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellDirection
{
    N, NE, E, SE, S, SW, W, NW
}

public static class CellDirectionsExtensions
{
    public static CellDirection Opposite(this CellDirection direction)
    {
        return (int)direction < 4 ? (direction + 4) : (direction - 4);
    }
}