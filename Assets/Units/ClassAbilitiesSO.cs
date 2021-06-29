using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Unit Data/Class Abilites Asset", fileName = "Default Class Abilities Asset")]

public class ClassAbilitiesSO : ScriptableObject
{
    [Title("Default Abilities for Classes")]

    public UnitClasses unitClass;
    public List<Ability> abilityList = new List<Ability>();
}
