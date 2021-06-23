using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Inventory System/Inventory", fileName = "New Inventory")]
public class Inventory : ScriptableObject
{
    public CompleteInventoryList completeInventoryList;
    public List<InventoryItem> heldInventory = new List<InventoryItem>();

    public string inventoryOwner;
    public bool isPartyMember;
    public bool isShop;

    public void AddToInventory(InventoryItem item)
    {
        heldInventory.Add(item);
    }

    public void RemoveFromInventory(InventoryItem item)
    {
        heldInventory.Remove(item);
    }
}
