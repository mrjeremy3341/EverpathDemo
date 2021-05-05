using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetBehaviour : MonoBehaviour
{
    public abstract GridCell[] GetTargets(GridCell targetCell);
}
