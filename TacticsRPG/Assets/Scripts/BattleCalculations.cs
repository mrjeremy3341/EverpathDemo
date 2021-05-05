using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BattleCalculations
{
    public static void SetInitiative(BattleUnit unit)
    {
        int r = Random.Range(0, 11);
        unit.unitStats.initiative = r + unit.unitStats.agility;
    }

    public static int BasicAttackDamage(BattleUnit attacker, BattleUnit defender, bool isMagic)
    {
        int attack;
        int defense;

        if (isMagic)
        {
            attack = attacker.unitStats.magic;
            defense = defender.unitStats.protection;
        }
        else
        {
            attack = attacker.unitStats.attack;
            defense = defender.unitStats.armor;
        }

        float a = 2 * (attack * attack);
        float d = attack + defense;
        float r = Random.Range(0.9f, 1.1f);
        float damage = (a / d) * r;

        return Mathf.FloorToInt(damage);
    }

    public static int DamageCalculation(BattleUnit attacker, BattleUnit defender, int damageAmmount, bool isMagic)
    {
        int attack;
        int defense;

        if (isMagic)
        {
            attack = attacker.unitStats.magic;
            defense = defender.unitStats.protection;
        }
        else
        {
            attack = attacker.unitStats.attack;
            defense = defender.unitStats.armor;
        }

        float a = 2 * (attack * damageAmmount);
        float d = attack + defense;
        float r = Random.Range(0.9f, 1.1f);
        float damage = (a / d) * r;

        return Mathf.FloorToInt(damage);
    }

    public static int HealCalculation(BattleUnit healer, int healAmmount)
    {
        int magic = healer.unitStats.magic;
        float r = Random.Range(0.9f, 1.1f);

        return Mathf.FloorToInt((magic + healAmmount) * r);
    }

    public static bool AttackHitChance(BattleUnit attacker, BattleUnit defender)
    {
        bool attackHit;
        int random = Random.Range(0, 100);
        int hitChance = ((attacker.unitStats.agility - defender.unitStats.agility) * 5) + 100;

        if(random > hitChance)
        {
            attackHit = false;
        }
        else
        {
            attackHit = true;
        }

        return attackHit;
    }
}