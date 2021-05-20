using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DataAsset/UnitData")]
public class UnitData : ScriptableObject
{
    public int maxHP;
    public int maxAP;
    public int maxMP;
    [Space]
    public int attack;
    public int magic;
    [Space]
    public int armor;
    public int protection;
    public int agility;
    [Space]
    public BaseAbility[] currentAbilites;
}
