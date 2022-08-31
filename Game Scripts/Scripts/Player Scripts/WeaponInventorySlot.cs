using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInventorySlot : MonoBehaviour
{
    PlayerInventory playerInventory;
    WeaponSlotManager weaponSlotManager;
    UIManager uiManager;

    public Image icon;
    WeaponItem item;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        uiManager = FindObjectOfType<UIManager>();
        weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
    }

    public void AddItem(WeaponItem newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearInventorySlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    public void EquipThisItem()
    {
        if (uiManager.rightHandSlot01Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponsInRightHandSlots[0]);
            playerInventory.weaponsInRightHandSlots[0] = item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if (uiManager.rightHandSlot02Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponsInRightHandSlots[1]);
            playerInventory.weaponsInRightHandSlots[1] = item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if (uiManager.rightHandSlot03Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponsInRightHandSlots[2]);
            playerInventory.weaponsInRightHandSlots[2] = item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if (uiManager.rightHandSlot04Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponsInRightHandSlots[3]);
            playerInventory.weaponsInRightHandSlots[3] = item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if(uiManager.leftHandSlot01Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponsInLeftHandSlots[0]);
            playerInventory.weaponsInLeftHandSlots[0] = item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if (uiManager.leftHandSlot02Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponsInLeftHandSlots[1]);
            playerInventory.weaponsInLeftHandSlots[1] = item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if (uiManager.leftHandSlot03Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponsInLeftHandSlots[2]);
            playerInventory.weaponsInLeftHandSlots[2] = item;
            playerInventory.weaponInventory.Remove(item);
        }
        else if (uiManager.leftHandSlot04Selected)
        {
            playerInventory.weaponInventory.Add(playerInventory.weaponsInLeftHandSlots[3]);
            playerInventory.weaponsInLeftHandSlots[3] = item;
            playerInventory.weaponInventory.Remove(item);
        }
        else
        {
            return;
        }

        playerInventory.rightWeapon = playerInventory.weaponsInRightHandSlots[playerInventory.currentRightWeaponIndex];
        playerInventory.leftWeapon = playerInventory.weaponsInLeftHandSlots[playerInventory.currentLeftWeaponIndex];
        
        weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon, true);

        uiManager.equipmentWindowUI.LoadWeaponOnEquipmentScreen(playerInventory);

        uiManager.ResetAllSelectedSlots();
    }
}
