using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Flags]
public enum Conditions { Stunned = 0, Slowed = 1, Sleep = 2, Invisible = 3, Taunted = 4, Silenced = 5 }

[CreateAssetMenu(fileName = "Unit Conditions")]
public class UnitConditionsSO : ScriptableObject
{
    public event EventHandler RefreshConditions;

   // we'll make it static later (didn't want to break/confuse)

    public bool stunned;        // later use dictionary
    public int stunnedTurns;

    public bool slowed;
    public int slowTurns;

    public bool sleep;
    public int sleepTurns;

    public bool invisible;
    public int invisTurns;

    public bool taunted;
    public int tauntedTurns;

    public bool silenced;
    public int silencedTurns;

    public List<string> immunities; // string to make things easier for now, later might refactor to a dictionary


    public void AttemptConditionApply(Ability ability, Conditions condition)
    {
        switch (condition)
        {
            case Conditions.Stunned:

                if (Stunnable(ability))
                {
                    stunned = true;
                    RefreshConditions?.Invoke(this, EventArgs.Empty); // Listened to by BattleUnit - BattleUnit doesn't necessarily have to do anything after listening. This is in case some execution has to take place after;
                    //calculate resistances (if mechanic exists)
                    //stunnedTurns = ability.duration; // (- ability.duration * resistances percentage, then round up or down)

                    // In the next turn whenever a unit wants to complete an action, it would reference with this SO to see if stunned. If so, itll get skipped, etc.
                }


                break;
            case Conditions.Slowed:
                break;
            case Conditions.Sleep:
                break;
            case Conditions.Invisible:
                break;
            case Conditions.Taunted:
                break;
            default:
                break;
        }
    }

    public void RecoverCondition(Conditions condition)
    {
        switch (condition)
        {
            case Conditions.Stunned:

                stunned = false;
                RefreshConditions?.Invoke(this, EventArgs.Empty);

                break;
            case Conditions.Slowed:
                break;
            case Conditions.Sleep:
                break;
            case Conditions.Invisible:
                break;
            case Conditions.Taunted:
                break;
            default:
                break;
        }
    }

    public void NextTurn()
    {
        // for DEMO - refactor later to make less wordy maybe;
        // To be called every turn for each BattleUnit

        stunnedTurns -= 1;
        slowTurns -= 1;
        sleepTurns -= 1;
        invisTurns -= 1;
        tauntedTurns -= 1;
        silencedTurns -= 1;

        if (stunnedTurns <= 0) { stunned = false; }
        if (slowTurns <= 0) { slowed = false; }
        if (sleepTurns <= 0) { sleep = false; }
        if (invisTurns <= 0) { invisible = false; }
        if (tauntedTurns <= 0) { taunted = false; }
        if (silencedTurns <= 0) { silenced = false; }
    }

    private bool Stunnable(Ability ability)
    {
        if (!ability.stackable && stunned)
        {
            return false;
        }
        if (immunities.Contains("Stun"))
        {
            return false;
        }

        return true;
    }
}
