using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Battle Unit Stats")]
public class UnitStatsSO : ScriptableObject
{
    // Create prefabs for all different units and during initialization change the stats (if stats require changing) else we can just instantiate as is.
    // We can easily change stats (with data persistency) for eg: when a battle starts before players rested and they start with half HP or whatever.

    public string unitName;
        
    public int maxHP;
    public int currentHP;
    public int maxAP;
    public int currentAP;
    public int maxMP;
    public int currentMP;
    public int attack;
    public int magic;
    public int armor;
    public int protection;
    public int agility;
    public int initiative;

    public GridCell unitCell;
    public bool actionUsed;
    public bool moveUsed;
    public int remainingTurnsForAction; // for enemy AI
}
