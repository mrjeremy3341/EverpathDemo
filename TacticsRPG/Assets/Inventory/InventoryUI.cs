using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class InventoryUI : MonoBehaviour
{
    public UIManager uiManager;
    public InventorySlot[] inventorySlots;    
    [ReadOnly]
    public int freeSlots;

    public bool isShop;
    ItemShop shop;
    Inventory shopInventory;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UIManager>();

        if (isShop)
        {
            shop = GetComponent<ItemShop>();
            shopInventory = shop.inventory;
            PopulateInventorySlots(shopInventory);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFreeSlots();
    }

    public void RelayUseItem(InventoryItem item)
    {
        if (!isShop)
        {
            uiManager.battleManager.turnManager.currentTurn.unitInventory.UseItem(item);
        }        
    }

    public void UpdateFreeSlots()
    {
        int occupiedSlots = 0;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].isOccupied)
            {
                occupiedSlots += 1;
            }
        }

        freeSlots = inventorySlots.Length - occupiedSlots;
    }

    public void PopulateInventorySlots(Inventory inventory)
    {
        if (isShop)
        {
            inventory = shopInventory;
        }

        ClearAllSlots();

        if (inventory.heldInventory.Count > inventorySlots.Length)
        {
            Debug.Log("Too many items");
        }
        else
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (i <= inventory.heldInventory.Count - 1)
                {
                    Debug.Log(i + " " + inventory.heldInventory[i]);
                    inventorySlots[i].item = inventory.heldInventory[i];
                    inventorySlots[i].slotImage.sprite = inventory.heldInventory[i].itemImage;
                    inventorySlots[i].itemText.text = inventory.heldInventory[i].itemName;
                    inventorySlots[i].isOccupied = true;
                }
                else
                {
                    inventorySlots[i].ClearSlot();
                }
            }
        }
    }

    public void ClearAllSlots()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].ClearSlot();
        }
    }
    
}
