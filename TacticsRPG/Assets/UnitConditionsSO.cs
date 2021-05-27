using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;


[CreateAssetMenu(fileName = "Unit Conditions")]
public class UnitConditionsSO : ScriptableObject
{
    //public event EventHandler RefreshConditions;

   // we'll make it static later (didn't want to break/confuse)

    [BoxGroup("Debuffs")]    
    public bool stunned;        // later use dictionary
    [BoxGroup("Debuffs")]    
    public int stunnedTurns;
    [BoxGroup("Debuffs")]    
    public bool slowed;
    [BoxGroup("Debuffs")]    
    public int slowTurns;
    [BoxGroup("Debuffs")]    
    public bool sleep;
    [BoxGroup("Debuffs")]    
    public int sleepTurns;
    [BoxGroup("Debuffs")]    
    public bool taunted;
    [BoxGroup("Debuffs")]    
    public int tauntedTurns;
    [BoxGroup("Debuffs")]    
    public bool silenced;
    [BoxGroup("Debuffs")]    
    public int silencedTurns;
    [BoxGroup("Debuffs")]    
    public bool dOT;
    [BoxGroup("Debuffs")]    
    public int dOTDamage;
    [BoxGroup("Debuffs")]    
    public int dOTTurns;

    [BoxGroup("Buffs")]    
    public bool invisible;
    [BoxGroup("Buffs")]    
    public int invisTurns;
    [BoxGroup("Buffs")]    
    public List<string> immunities = new List<string>(); // string to make things easier for now, later might refactor to a dictionary


    public void AttemptConditionApply(Ability ability, Conditions condition)    // always apply conditions via this
    {
        Debug.Log("Attempting Conditions apply");
        Debug.Log(condition);

        switch (condition)
        {
            case Conditions.Stunned:

                Debug.Log("Switched to Stun");
                Debug.Log(Stunnable(ability));

                if (Stunnable(ability))
                {
                    stunned = true;
                    stunnedTurns = ability.stunnedDuration;
                    //RefreshConditions?.Invoke(this, EventArgs.Empty); // Listened to by BattleUnit - BattleUnit doesn't necessarily have to do anything after listening. This is in case some execution has to take place after;
                    Debug.Log("Stun Applied");
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
            case Conditions.DamageOverTime:

                dOTDamage += ability.damageOverTime;
                dOTTurns = ability.dOTDuration;

                break;
            default:
                break;
        }
    }

    public void RecoverCondition(Conditions condition)  // For healing spells and shit
    {
        switch (condition)
        {
            case Conditions.Stunned:

                stunned = false;
                stunnedTurns = 0;                

                break;
            case Conditions.Slowed:
                break;
            case Conditions.Sleep:
                break;
            case Conditions.Invisible:
                break;
            case Conditions.Taunted:
                break;
            case Conditions.DamageOverTime:
                break;
            default:
                break;
        }
    }

    public void DropDuration()
    {
        // for DEMO - refactor later to make less wordy maybe;
        // To be called every turn for each BattleUnit

        stunnedTurns -= 1;
        slowTurns -= 1;
        sleepTurns -= 1;
        invisTurns -= 1;
        tauntedTurns -= 1;
        silencedTurns -= 1;
        dOTTurns -= 1;

        if (stunnedTurns <= 0) { stunned = false; stunnedTurns = 0; }
        if (slowTurns <= 0) { slowed = false; slowTurns = 0; }
        if (sleepTurns <= 0) { sleep = false; sleepTurns = 0; }
        if (invisTurns <= 0) { invisible = false; invisTurns = 0; }
        if (tauntedTurns <= 0) { taunted = false; tauntedTurns = 0; }
        if (silencedTurns <= 0) { silenced = false; silencedTurns = 0; }
        if (dOTTurns <= 0) { dOT = false; dOTTurns = 0; }
    }

    public bool Stunnable(Ability ability)
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
