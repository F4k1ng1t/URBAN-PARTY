using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum ItemType
{
    Weapon,
    Armor
}
public class InventorySlotController : MonoBehaviour
{
    public bool debugFill;
    public bool isFilled = false;
    public ItemType type;
    public Image itemImage;
    public int libraryIndex;
    public int quantity;

    private void Awake()
    {
        itemImage = GetComponent<Image>();
    }

    private void Start()
    {
        if (debugFill)
        {
            SetInventorySlot(ItemType.Weapon, 0, 1);
        }
    }

    public void SetInventorySlot(ItemType addType, int addIndex, int addQuantity)
    {
        //checks for invalid idex
        if (addIndex < 0)
        {
            return;
        }
        type = addType;
        libraryIndex = addIndex;
        quantity = addQuantity;
        isFilled = true;
        if(type == ItemType.Weapon)
        {
            itemImage.sprite = ItemLibrary.weaponLibrary[libraryIndex].itemSprite;
        }
    }

    /// <summary>
    /// copy another inventory slot
    /// </summary>
    /// <param name="other"></param>
    public void SetInventorySlot(InventorySlotController other)
    {
        //Debug.Log(other.libraryIndex);
        if (other.libraryIndex == -1)
        {
            return;
        }
        type = other.type;
        libraryIndex = other.libraryIndex;
        quantity = other.quantity;
        isFilled = true;

        if (type == ItemType.Weapon)
        {
            itemImage.sprite = ItemLibrary.weaponLibrary[libraryIndex].itemSprite;
        }
    }

    public void EmptyInventorySlot()
    {
        libraryIndex = -1;
        isFilled = false;
        //Debug.Log(itemImage.sprite);
        itemImage.sprite = null;
    }

    public void OnItemSelect()
    {
        //if a slot with a filled inventory slot is selected, place it into the players "hand"
        if (InventoryController.hand == null && isFilled)
        {
            InventoryController.hand = this;
        }
        else if (InventoryController.hand != null)
        {
            //if the player selects an empty slot, place the item there
            if (!isFilled)
            {
                SetInventorySlot(InventoryController.hand);
                InventoryController.hand.EmptyInventorySlot();
                InventoryController.hand = null;
            }
        }
    }
}
