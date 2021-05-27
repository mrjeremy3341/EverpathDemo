using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Unit Data/List Containers/Default Enemy Class Abilities", fileName = "Default Enemy Class Abilities List")]

public class DefaultEnemyClassAbilities : SerializedScriptableObject
{
    public Dictionary<EnemyClass, ClassAbilitiesSO> defaultAbilityList = new Dictionary<EnemyClass, ClassAbilitiesSO>();
}
