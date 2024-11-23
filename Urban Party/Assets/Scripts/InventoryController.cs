using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private InventorySlotController[] slots;
    public static InventorySlotController hand;

    private void Start()
    {
        slots = gameObject.GetComponentsInChildren<InventorySlotController>();
    }


    public void AddItem(int index, bool isArmor)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].isFilled)
            {
                return;
            }
        }
        Debug.Log("Inventory Full");
    }
}
