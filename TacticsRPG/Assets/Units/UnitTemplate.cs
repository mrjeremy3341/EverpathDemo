using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum UnitClasses
{
    Scout,
    Soldier,
    Acolyte,
    Apprentice
}

[CreateAssetMenu(menuName = "Unit Data/Unit Template", fileName = "New Unit Template")]

public class UnitTemplate : SerializedScriptableObject
{
    public string unitName;
    [PreviewField(55, ObjectFieldAlignment.Left)]
    public Sprite unitSprite;

    public UnitClasses unitClass;

    [BoxGroup("Scaling")]
    [AssetList(Path = "Units/Default Dictionaries/")]
    public DefaultScalingParameters scalingParameters;
    [InfoBox("If unit uses custom scaling every level up")]
    [BoxGroup("Scaling")]
    public bool usesCustomScaling;
    [ShowIf("usesCustomScaling")]
    [BoxGroup("Scaling")]
    public ScalingStatsSO customScalingParameters;
    [BoxGroup("Abilities")]
    [AssetList(Path = "Units/Default Dictionaries/")]
    public DefaultEnemyClassAbilities classAbilities;
    [InfoBox("If unit has custom abilities")]
    [BoxGroup("Abilities")]
    public bool usesCustomAbilities;
    [ShowIf("usesCustomAbilities")]
    [BoxGroup("Abilities")]
    public ClassAbilitiesSO customClassAbilities;

    [BoxGroup("Base Stats")]
    public int basicAttackRange;
    [BoxGroup("Base Stats")]
    public bool basicAttackIsMagic;
    [BoxGroup("Base Stats")]
    public int baseHP;
    [BoxGroup("Base Stats")]
    public int baseAP;
    [BoxGroup("Base Stats")]
    public int baseMP;
    [BoxGroup("Base Stats")]
    public int baseAttack;
    [BoxGroup("Base Stats")]
    public int baseArmor;
    [BoxGroup("Base Stats")]
    public int baseProtection;
    [BoxGroup("Base Stats")]
    public int baseAgility;

    public List<Conditions> baseImmunities = new List<Conditions>();
}
