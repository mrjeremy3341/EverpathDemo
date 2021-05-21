using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilities/Hammer of Dawn", fileName ="Hammer of Dawn")]
public class HammerOfDawn : Ability
{
    public override bool AbilityAuthorized(GridCell targetCell)
    {
        return false;
    }

    public override void Execute(GridCell targetCell)
    {

    }

}
