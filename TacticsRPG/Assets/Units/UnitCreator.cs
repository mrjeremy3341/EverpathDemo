using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitCreator
{
    public static void BuildUnit(BattleUnit unit, UnitTemplate template)
    {
        UnitStats stats = unit.GetComponent<UnitStats>();
        stats.maxHP = template.baseHP;
        stats.maxAP = template.baseAP;
        stats.maxMP = template.baseMP;
        stats.protection = template.baseProtection;
        stats.attack = template.baseAttack;
        stats.armor = template.baseArmor;
        stats.agility = template.baseAgility;
        stats.unitName = template.unitName;

        var sr = unit.GetComponentInChildren<SpriteRenderer>();
        sr.sprite = template.unitSprite;
        UnitConditionsSO _unitConditions = ScriptableObject.CreateInstance<UnitConditionsSO>();
        unit.unitConditions = _unitConditions;
        var unitAbilities = unit.GetComponent<UnitAbilities>();
        unitAbilities.unitAbilities = template.classAbilities.abilityList;
        unit.GetComponentInChildren<Animator>().runtimeAnimatorController = template.animatorController;
        unit.GetComponent<UnitInventory>().inventory = template.inventory;

        //stats.maxHP = ScaleHP(template);
        //stats.maxAP = ScaleAP(template);
    }

    /*
    private static int ScaleHP(UnitTemplate template)
    {
        float strength = (template.scalingParameters.defaultLevellingParameters[template.enemyClass].strength) * 0.6f;
        float constitution = template.scalingParameters.defaultLevellingParameters[template.enemyClass].constitution;
        int hP = Mathf.RoundToInt(template.baseHP + (template.baseHP * ((strength + constitution) / 100)));
        return hP;
    }
    private static int ScaleAP(UnitTemplate template)
    {
        float intelligence = template.scalingParameters.defaultLevellingParameters[template.enemyClass].intelligence * 0.9f;
        int aP = Mathf.RoundToInt(template.baseAP + ((template.baseAP * intelligence)/100));
        return aP;
    }
    */
}
