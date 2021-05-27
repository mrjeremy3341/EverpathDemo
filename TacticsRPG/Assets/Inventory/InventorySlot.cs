using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public InventoryItem item;
    public bool isOccupied;

    public TextMeshProUGUI itemText;
    public Image slotImage;
    Button slotButton;    

    // Start is called before the first frame update
    void Start()
    {
        inventoryUI = GetComponentInParent<InventoryUI>();
        slotButton = GetComponent<Button>();
        slotImage = GetComponent<Image>();
        itemText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        DeactivatePassiveSlots();
    }

    public void OnItemClick()
    {
        if (!item.isPassive)
        {
            inventoryUI.RelayUseItem(item);
        }
    }

    public void ClearSlot()
    {
        item = null;
        isOccupied = false;
        itemText.text = "Empty Item Slot";
        slotImage.sprite = null;
        slotButton.enabled = false;
    }

    private void DeactivatePassiveSlots()
    {
        if (isOccupied)
        {
            if (item.isPassive)
            {
                slotButton.enabled = false;
            }
            else
            {
                slotButton.enabled = true;
            }
        }
    }
}
