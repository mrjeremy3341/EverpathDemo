using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemShop : MonoBehaviour
{
    public InventoryUI inventoryUI;

    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UseItem(InventoryItem item)
    {
        Debug.Log(item.name + " Used");
        inventory.RemoveFromInventory(item);
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        if (inventoryUI.isShop)
        {
            if (inventory != null)
            {
                if (inventory.isPartyMember)
                {
                    inventoryUI.PopulateInventorySlots(inventory);
                }
                else
                {
                    inventoryUI.ClearAllSlots();
                }
            }
        }

    }
}
