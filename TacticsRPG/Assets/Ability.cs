using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public enum TargetType
{
    Ground = 1,
    Ally = 2,
    Enemy = 4
}

public enum TargetBehaviors
{
    SingleTarget,
    SelfTarget,
    areaOfEffect,
    GridCell
}

[CreateAssetMenu(menuName = "Abilities/New Ability")]
public class Ability : SerializedScriptableObject
{
    public string abilityName;

    public float apCost;
    public bool stackable;

    [FoldoutGroup("Base Parameters")]
    [Range(1, 3)]
    public int level = 1;
    [FoldoutGroup("Base Parameters")]
    public bool requiresTarget;
    [FoldoutGroup("Base Parameters")]
    public bool onlyTargetDamaged = false;
    [FoldoutGroup("Base Parameters")]
    public TargetType baseTargetType;
    [FoldoutGroup("Base Parameters")]
    public bool multipleTargetBehaviours;
    [FoldoutGroup("Base Parameters")]
    public int targettingRange;

    public List<Conditions> blockingConditions;

    [FoldoutGroup("Ability Effects")]
    [HorizontalGroup("Ability Effects/Lists")]
    [AssetSelector(Paths = "/Prefabs/Behaviour Prefabs")]
    public List<AbilityBehavior> abilityBehaviors;
    [HorizontalGroup("Ability Effects/Lists")]
    public List<TargetBehaviors> targetBehaviours;
    [HorizontalGroup("Ability Effects/Lists")]
    public List<Conditions> conditions;
    [DetailedInfoBox("Every ability behaviour must have a corresponding target behaviour and every ApplyConditions needs a corresponding behavior, target type and condition", 
        "Executions are run abilityBehaviour[i] and targetBehaviour[i])] therefore both Lists must be the same size. " +
        "The Conditions list does not need to be the same size as the other two however it runs with every ApplyCondition behavior in the behaviours list")]
    public bool isSpecializedAbility;

    [InfoBox("If consecutive abilities have modified values, include them here")]
    public bool hasAdditionalModifiers;
    [ShowIfGroup("hasAdditionalModifiers")]
    [FoldoutGroup("hasAdditionalModifiers/Modifiers")]
    [HorizontalGroup("hasAdditionalModifiers/Modifiers/Lists")]
    public List<int> rangeModifier;
    [HorizontalGroup("hasAdditionalModifiers/Modifiers/Lists")]
    public List<int> damageModifier;
    [HorizontalGroup("hasAdditionalModifiers/Modifiers/Lists")]
    public List<int> healModifier;

    [ReadOnly]
    public BattleUnit battleUnit;
    protected BattleActions battleActions;

    [FoldoutGroup("Heal")]
    public int healAmount;
    [FoldoutGroup("Heal")]
    public int pulses;

    [FoldoutGroup("Damage")]
    public int rawDamage;
    [FoldoutGroup("Damage")]
    public bool isMagic;

    public bool appliesConditions;
    [ShowIfGroup("appliesConditions")]
    [FoldoutGroup("appliesConditions/Conditions Applied")]
    [HorizontalGroup("appliesConditions/Conditions Applied/Stun")]
    public int stunDelay;
    [HorizontalGroup("appliesConditions/Conditions Applied/Stun")]
    public int stunnedDuration;
    [HorizontalGroup("appliesConditions/Conditions Applied/Slow")]
    public int slowDelay;
    [HorizontalGroup("appliesConditions/Conditions Applied/Slow")]
    public int slowDuration;
    [HorizontalGroup("appliesConditions/Conditions Applied/Sleep")]
    public int sleepDelay;
    [HorizontalGroup("appliesConditions/Conditions Applied/Sleep")]
    public int sleepDuration;
    [HorizontalGroup("appliesConditions/Conditions Applied/Invisiblity")]
    public int invisibleDelay;
    [HorizontalGroup("appliesConditions/Conditions Applied/Invisiblity")]
    public int invisDuration;
    [HorizontalGroup("appliesConditions/Conditions Applied/Taunt")]
    public int tauntDelay;
    [HorizontalGroup("appliesConditions/Conditions Applied/Taunt")]
    public int tauntedDuration;
    [HorizontalGroup("appliesConditions/Conditions Applied/Silence")]
    public int silenceDelay;
    [HorizontalGroup("appliesConditions/Conditions Applied/Silence")]
    public int silencedDuration;
    

    public virtual bool AbilityAuthorized(GridCell targetCell)
    {
        //if (battleUnit.unitStats.currentAP < apCost) { Debug.Log("Not enough AP");  return false; }
        //if (battleUnit.unitStats.actionUsed) { Debug.Log("Action already used"); return false; }
        if (blockingConditions.Count != 0)
        {
            for (int i = 0; i < blockingConditions.Count; i++)
            {
                Debug.Log("Checking Condition Blocks");
                if (blockingConditions[i] == Conditions.Stunned && battleUnit.unitConditions.stunned) { return false; }
                if (blockingConditions[i] == Conditions.Slowed && battleUnit.unitConditions.slowed) { return false; }
                if (blockingConditions[i] == Conditions.Sleep && battleUnit.unitConditions.sleep) { return false; }
                if (blockingConditions[i] == Conditions.Invisible && battleUnit.unitConditions.invisible) { return false; }
                if (blockingConditions[i] == Conditions.Taunted && battleUnit.unitConditions.taunted) { return false; }
                if (blockingConditions[i] == Conditions.Silenced && battleUnit.unitConditions.silenced) { return false; }
            }
        }

        return true;
    }

    public virtual void Execute(GridCell targetCell, GameObject thisObj)
    {
        foreach (var behaviour in abilityBehaviors)
        {
            //behaviour.Execute(targetCell);
        }
    }
}
