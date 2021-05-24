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

public enum AbilityEffects
{
    None,
    HealEffect,
    DamageEffect,
    MoveEffect,
    PushEffect,
    ApplyCondition,
    SpawnPrefab
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
    public List<AbilityEffects> abilityEffects;
    [HorizontalGroup("Ability Effects/Lists")]
    public List<TargetBehaviors> targetBehaviours;
    [HorizontalGroup("Ability Effects/Lists")]
    public List<Conditions> conditions;
    
    [InfoBox("EXPERIMENTAL")]
    public bool hasStackingAbilities;
    [ShowIf("hasStackingAbilities")]
    public List<Ability> stackingAbilities;

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
    [HorizontalGroup("appliesConditions/Conditions Applied/Damage Over Time")]
    public int damageOverTime;
    [HorizontalGroup("appliesConditions/Conditions Applied/Damage Over Time")]
    public int dOTDuration;

    public string preparationAnimationParameter;
    public GameObject preparationParticleEffect;
    public string executeAnimationParameter;
    public GameObject executeParticleEffect;


    public virtual bool AbilityAuthorized(GridCell targetCell)
    {
        if (blockingConditions.Count != 0)
        {
            for (int i = 0; i < blockingConditions.Count; i++)
            {
                Debug.Log("Checking Condition Blocks");
                if (blockingConditions[i] == Conditions.Stunned && battleUnit.battleConditions.CheckCondition(Conditions.Stunned)) { return false; }
                if (blockingConditions[i] == Conditions.Slowed && battleUnit.battleConditions.CheckCondition(Conditions.Slowed)) { return false; }
                if (blockingConditions[i] == Conditions.Sleep && battleUnit.battleConditions.CheckCondition(Conditions.Sleep)) { return false; }
                if (blockingConditions[i] == Conditions.Invisible && battleUnit.battleConditions.CheckCondition(Conditions.Invisible)) { return false; }
                if (blockingConditions[i] == Conditions.Taunted && battleUnit.battleConditions.CheckCondition(Conditions.Taunted)) { return false; }
                if (blockingConditions[i] == Conditions.Silenced && battleUnit.battleConditions.CheckCondition(Conditions.Silenced)) { return false; }
                if (blockingConditions[i] == Conditions.DamageOverTime && battleUnit.battleConditions.CheckCondition(Conditions.DamageOverTime)) { return false; }
            }
        }

        return true;
    }
}
