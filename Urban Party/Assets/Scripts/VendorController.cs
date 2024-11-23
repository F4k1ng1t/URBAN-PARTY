using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VendorController : MonoBehaviour
{
    public static VendorController instance;

    private int price;
    private int index;
    private ItemType type;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI vendorText;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }

    public void SetItemToSell(ItemType setType, int setIndex)
    {
        type = setType;
        index = setIndex;

        if(type == ItemType.Weapon)
        {
            WeaponData data = ItemLibrary.weaponLibrary[index];
            vendorText.text = "Item: " + data.itemName + "\nDamage: " + data.damage + "\nSpeed: " + data.speed;
            buttonText.text = "Buy:\n" + data.value + "\nconfetti";
        }
        else if(type == ItemType.Armor)
        {
            Debug.LogError("Not Yet Implemented");
        }
        
    }

    public void BuyItem()
    {
        if (PlayerController.instance.confetti > ItemLibrary.weaponLibrary[index].value)
        {
            PlayerController.instance.playerInventory.AddItem(type, index, 1);
            PlayerController.instance.confetti -= ItemLibrary.weaponLibrary[index].value;
        }
        else
        {
            Debug.Log("Poor");
        }
    }
}
