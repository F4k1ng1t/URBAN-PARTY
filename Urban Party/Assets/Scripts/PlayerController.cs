using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public InventoryController playerInventory;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }
}
