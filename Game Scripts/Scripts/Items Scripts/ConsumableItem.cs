using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    [Header("Item Quantity")]
    public int maxItemAmount;
    public int currentItemAmount;

    [Header("Item Model")]
    public GameObject itemModel;

    [Header("Item Animations")]
    public string consumableAnimation;
    public string emptyAnimation;
    public bool isInteracting;

    public virtual void AttemptToConsumeTheItem(PlayerAnimatorHandler playerAnimatorHandler, WeaponSlotManager weaponSlotManager, PlayerFXManager playerFXManager)
    {
        if(currentItemAmount > 0)
        {
            playerAnimatorHandler.PlayTargetAnimation("Potion_Drink", isInteracting, true);            
        }
        else if (currentItemAmount <= 0)
        {
            playerAnimatorHandler.PlayTargetAnimation("Potion_Empty", true);
        }
    }
}
