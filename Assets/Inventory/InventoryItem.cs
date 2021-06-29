using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Item", fileName = "New Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public bool isPassive;

    public int healthIncrease;
    public int damageIncrease;
    public int agilityIncrease;
}
