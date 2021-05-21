using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Flags]
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
    AoE,
    GridCell
}

[CreateAssetMenu(menuName = "Abilities/New Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public bool isSpecializedAbility;
    public float apCost;
    public bool stackable;

    [EnumFlagsAttribute]
    public Conditions blockingConditions;

    public List<AbilityBehavior> abilityBehaviors;

    [Header("Base Ability")]
    [Range(1, 3)]
    public int level = 1;
    public bool requiresTarget;
    public bool onlyTargetDamaged = false;
    public TargetType targetType;
    public TargetBehaviors targetBehaviour;
    public bool multipleTargetBehaviours;
    public int range;

    [Header("Heal Behaviour")]
    public bool hasHealBehaviour;
    public float healAmount;
    public int pulses;

    [Header("Damage")]
    public bool hasDamageBehavior;
    public float rawDamage;
    public bool isMagic;

    [Header("Conditions Applied")]
    public bool stun;        
    public int stunnedDuration;
    public bool slow;
    public int slowDuration;
    public bool sleep;
    public int sleepDuration;
    public bool invisible;
    public int invisDuration;
    public bool taunt;
    public int tauntedDuration;
    public bool silence;
    public int silencedDuration;
    
    protected BattleUnit battleUnit;
    protected BattleActions battleActions;

    public List<int> SelectedBlockingConditions()
    {
        List<int> selected = new List<int>();
        for (int i = 0; i < Enum.GetValues(typeof(Conditions)).Length; i++)
        {
            int layer = 1 << i;
            if (((int)blockingConditions & layer) != 0)
            {
                selected.Add(i);
            }            
        }

        return selected;
    }

    public virtual bool AbilityAuthorized(GridCell targetCell)
    {
        if (battleUnit.unitInfo.currentAP < apCost) { return false; }
        if (battleUnit.unitInfo.actionUsed) { return false; }
        for (int i = 0; i < SelectedBlockingConditions().Count; i++)
        {
            if (SelectedBlockingConditions()[i] == 0 && battleUnit.unitConditions.stunned ) { return false; }
            if (SelectedBlockingConditions()[i] == 1 && battleUnit.unitConditions.slowed) { return false; }
            if (SelectedBlockingConditions()[i] == 2 && battleUnit.unitConditions.sleep) { return false; }
            if (SelectedBlockingConditions()[i] == 3 && battleUnit.unitConditions.invisible) { return false; }
            if (SelectedBlockingConditions()[i] == 4 && battleUnit.unitConditions.taunted) { return false; }
            if (SelectedBlockingConditions()[i] == 5 && battleUnit.unitConditions.silenced) { return false; }
        }

        return true;
    }

    public virtual void Execute(GridCell targetCell)
    {

    }


    public void Foo()
    {
        for (int i = 0; i < SelectedBlockingConditions().Count; i++)
        {
            Debug.Log(SelectedBlockingConditions()[i].ToString());
            Debug.Log(SelectedBlockingConditions()[i]);
        }
    }

}
