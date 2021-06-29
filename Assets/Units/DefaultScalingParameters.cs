using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Unit Data/List Containers/Default Scaling Parameters", fileName = "Scaling Parameters List")]

public class DefaultScalingParameters : SerializedScriptableObject
{
    public Dictionary<EnemyClass, ScalingStatsSO> defaultLevellingParameters = new Dictionary<EnemyClass, ScalingStatsSO>();
}
