using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Unit Data/Scaling Parameters Asset", fileName = "New Class Scaling Parameters")]

public class ScalingStatsSO : ScriptableObject
{
    [Title("Default Levelling Stats for Classes")]

    public EnemyClass enemyClass;

    public float strength;
    public float intelligence;
    public float constitution;
    public float slipperiness;
    public float luck;
}
