using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IsoHelperFunctions
{
    public static Vector3 IsoToXY(Vector3 v)
    {
        return new Vector3(0.5f * v.x - v.y, 0.5f * v.x + v.y, 0f);
    }

    public static Vector3 XYToIso(Vector3 v)
    {
        return new Vector3(v.x + v.y, 0.5f * (v.y - v.x), 0f);
    }
}