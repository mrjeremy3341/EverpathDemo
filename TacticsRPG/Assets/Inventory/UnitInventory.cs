using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class UnitInventory : MonoBehaviour
{
    public Inventory inventory;
    public InventoryUI inventoryUI;

    [InlineButton("UpdateInventoryUI")]
    public bool empty;

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
        if (!inventoryUI.isShop)
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
        else
        {
            inventoryUI.PopulateInventorySlots(inventory);
        }
    }
}
