using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private InventorySlotController[] slots;
    public static InventorySlotController hand;
    public bool vendorInventory;

    private void Start()
    {
        slots = gameObject.GetComponentsInChildren<InventorySlotController>();
        foreach (InventorySlotController slot in slots)
        {
            slot.parentInventory = this;
        }
    }


    public void AddItem(ItemType addType, int addIndex, int addQuantity)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].isFilled)
            {
                slots[i].SetInventorySlot(addType, addIndex, addQuantity);
            }
        }
        Debug.Log("Inventory Full");
    }
}
