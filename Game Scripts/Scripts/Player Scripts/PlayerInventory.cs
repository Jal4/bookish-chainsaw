using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    WeaponSlotManager weaponSlotManager;

    [Header("Quick Slot Items")]
    public ConsumableItem currentConsumable;
    public SpellItem currentSpell;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;

    public WeaponItem unarmedWeapon;

    public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[1];
    public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[1];

    [Header("Current Equipment")]
    public HelmetEquipmentScript currentHelmetEquipment;
    public TorsoEquipmentScript currentTorsoEquipment;
    public LegEquipmentScript currentLegEquipment;
    public HandEquipmentScript currentHandEquipment;

    public int currentRightWeaponIndex = 1;
    public int currentLeftWeaponIndex = 1;

    public List<WeaponItem> weaponInventory;

    private void Awake()
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }
    private void Start()
    {
        rightWeapon = weaponsInRightHandSlots[1];
        leftWeapon = weaponsInLeftHandSlots[1];
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, true);
    }
    public void ChangeRightWeapon()
    {
        currentRightWeaponIndex = currentRightWeaponIndex + 1;

        if (currentRightWeaponIndex > weaponsInRightHandSlots.Length - 1)
        {
            currentRightWeaponIndex = -1;
            rightWeapon = unarmedWeapon;
            weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);
        }

        else if (weaponsInRightHandSlots[currentRightWeaponIndex] != null)

        {
            rightWeapon = weaponsInRightHandSlots[currentRightWeaponIndex];

            weaponSlotManager.LoadWeaponOnSlot(weaponsInRightHandSlots[currentRightWeaponIndex], false);
        }

        else
        {
            currentRightWeaponIndex = currentRightWeaponIndex + 1;
        }
    }
    public void ChangeLeftWeapon()
    {
        currentLeftWeaponIndex = currentLeftWeaponIndex + 1;

        if (currentLeftWeaponIndex > weaponsInLeftHandSlots.Length - 1)
        {
            currentLeftWeaponIndex = -1;
            leftWeapon = unarmedWeapon;
            weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, true);
        }

        else if (weaponsInLeftHandSlots[currentLeftWeaponIndex] != null)

        {
            leftWeapon = weaponsInLeftHandSlots[currentLeftWeaponIndex];

            weaponSlotManager.LoadWeaponOnSlot(weaponsInLeftHandSlots[currentLeftWeaponIndex], true);
        }

        else
        {
            currentLeftWeaponIndex = currentLeftWeaponIndex + 1;
        }
    }
}
