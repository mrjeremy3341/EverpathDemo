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


    // Start is called before the first frame update
    void Start()
    {
        uiManager = GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateFreeSlots();
    }

    public void RelayUseItem(InventoryItem item)
    {
        uiManager.battleManager.turnManager.currentTurn.unitInventory.UseItem(item);
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

    public void PopulateInventorySlots(Inventory unitInventory)
    {
        ClearAllSlots();

        if (unitInventory.heldInventory.Count > inventorySlots.Length)
        {
            Debug.Log("Too many items");
        }
        else
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {
                if (i <= unitInventory.heldInventory.Count - 1)
                {
                    Debug.Log(i + " " + unitInventory.heldInventory[i]);
                    inventorySlots[i].item = unitInventory.heldInventory[i];
                    inventorySlots[i].slotImage.sprite = unitInventory.heldInventory[i].itemImage;
                    inventorySlots[i].itemText.text = unitInventory.heldInventory[i].itemName;
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
