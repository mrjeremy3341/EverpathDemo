using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBehavior : MonoBehaviour
{
    public abstract void Execute(GridCell targetCell);
}
