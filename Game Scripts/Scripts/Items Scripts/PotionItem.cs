using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumables/PotionItem")]

public class PotionItem : ConsumableItem
{
    [Header("Potion Type")]
    public bool HealthPotion;
    public bool MagicPotion;

    [Header("Recovery Amount")]
    public int healthRecoveryAmount;
    public int focusPointsRecoveryAmount;

    [Header("Recovery FX")]
    public GameObject recoveryFX;

    public override void AttemptToConsumeTheItem(PlayerAnimatorHandler playerAnimatorHandler, WeaponSlotManager weaponSlotManager, PlayerFXManager playerFXManager)
    {
        base.AttemptToConsumeTheItem (playerAnimatorHandler, weaponSlotManager, playerFXManager);
        GameObject Potion = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
        playerFXManager.currentVFX = recoveryFX;
        playerFXManager.amountToBeHealed = healthRecoveryAmount;
        playerFXManager.instantiatedModelFX = Potion;
        weaponSlotManager.rightHandSlot.UnloadWeapon();
    }
}
