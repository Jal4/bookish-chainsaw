using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Networking;

public class PlayerAttacker : MonoBehaviour
{
    PlayerAnimatorHandler animatorHandler;
    PlayerEquipmentManager equipmentManager;
    PlayerInventory playerInventory;
    PlayerManager playerManager;
    InputHandler inputHandler;
    WeaponSlotManager weaponSlotManager;
    PlayerStats playerStats;

    LayerMask counterLayer = 1 << 15;
    LayerMask backStabLayer = 1 << 14;

    public string lastAttack;
    CriticalDamageCollider backStabCollider;

    private void Awake()
    {
        equipmentManager = GetComponent<PlayerEquipmentManager>();
        inputHandler = GetComponentInParent<InputHandler>();
        animatorHandler = GetComponent<PlayerAnimatorHandler>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
        playerManager = GetComponentInParent<PlayerManager>();
        playerInventory = GetComponentInParent<PlayerInventory>();
        playerStats = GetComponentInParent<PlayerStats>();
    }
    public void HandleLightMeleeAction(WeaponItem weapon)
    {
        if (playerStats.currentStamina <= 0)
            return;
        weaponSlotManager.attackingWeapon = weapon;

        if (inputHandler.twoHandFlag)
        {
            animatorHandler.PlayTargetAnimation(weapon.TH_Light_Attack_01, true);
            lastAttack = weapon.TH_Light_Attack_01;
        }
        else
        {
            animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_01, true);
            lastAttack = weapon.OH_Light_Attack_01;
        }
    }
    public void HandleHeavyMeleeAttack(WeaponItem weapon)
    {
        if (playerStats.currentStamina <= 0)
            return;

        weaponSlotManager.attackingWeapon = weapon;
        if (inputHandler.twoHandFlag)
        {
            animatorHandler.PlayTargetAnimation(weapon.TH_Heavy_Attack_01, true);
            lastAttack = weapon.TH_Heavy_Attack_01;
        }
        else
        {
            animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_01, true);
            lastAttack = weapon.OH_Heavy_Attack_01;
        }
    }

    public void HandleWeaponCombo(WeaponItem weapon)
    {
        if (playerStats.currentStamina <= 0)
            return;

        if (inputHandler.comboFlag)
        {
            if (inputHandler.twoHandFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == weapon.TH_Light_Attack_01)
                {
                    animatorHandler.PlayTargetAnimation(weapon.TH_Light_Attack_02, true);
                    lastAttack = weapon.TH_Light_Attack_02;
                }
                
                else if (lastAttack == weapon.TH_Light_Attack_02)
                {
                    animatorHandler.PlayTargetAnimation(weapon.TH_Light_Attack_01, true);
                    lastAttack = weapon.TH_Light_Attack_01;
                }
                
                else if (lastAttack == weapon.TH_Heavy_Attack_01)
                {
                    animatorHandler.PlayTargetAnimation(weapon.TH_Heavy_Attack_02, true);
                    lastAttack = weapon.TH_Heavy_Attack_02;
                }
                
                else if (lastAttack == weapon.TH_Heavy_Attack_02)
                {
                    animatorHandler.PlayTargetAnimation(weapon.TH_Heavy_Attack_01, true);
                    lastAttack = weapon.TH_Heavy_Attack_01;
                }
            }
           
            else if (!inputHandler.twoHandFlag)
            {
                animatorHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == weapon.OH_Light_Attack_01)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_02, true);
                    lastAttack = weapon.OH_Light_Attack_02;
                }

                else if (lastAttack == weapon.OH_Light_Attack_02)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_01, true);
                    lastAttack = weapon.OH_Light_Attack_01;
                }

                else if (lastAttack == weapon.OH_Heavy_Attack_01)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_02, true);
                    lastAttack = weapon.OH_Heavy_Attack_02;
                }
                
                else if (lastAttack == weapon.OH_Heavy_Attack_02)
                {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_01, true);
                    lastAttack = weapon.OH_Heavy_Attack_01;
                }
            }

        }
    }
    #region InputActions
    public void HandleLightAction()
    {
        if (playerInventory.rightWeapon.isMeleeWeapon)
        {
            PerformLightMeleeAction();
        }
        else if (playerInventory.rightWeapon.isSpellCaster || playerInventory.rightWeapon.isFaithCaster || playerInventory.rightWeapon.isPyroCaster)
        {
            PerformLightMagicAction(playerInventory.rightWeapon);
        }
    }

    public void HandleHeavyAction()
    {
        if(playerInventory.rightWeapon.isMeleeWeapon)
        {
            PerformHeavyMeleeAction();
        }
        else if (playerInventory.rightWeapon.isSpellCaster || playerInventory.rightWeapon.isFaithCaster || playerInventory.rightWeapon.isPyroCaster)
        {
            return;
        }
    }
    
    public void HandleBlockAction()
    {
        PerformBlockAction();
    }

    public void HandleParryAction()
    {
        if (playerInventory.leftWeapon.isShieldWeapon)
        {
            PerformParryWeaponArt(inputHandler.twoHandFlag);
        }
        else if (playerInventory.leftWeapon.isMeleeWeapon)
        {
            //do a light attack
        }
    }

    #endregion
    #region Attack Actions
    private void PerformLightMeleeAction()
    {
        if (playerManager.canDoCombo)
        {
            inputHandler.comboFlag = true;
            HandleWeaponCombo(playerInventory.rightWeapon);
            inputHandler.comboFlag = false;
        }
        else
        {
            if (playerManager.isInteracting)
                return;
            if (playerManager.canDoCombo)
                return;

            animatorHandler.anim.SetBool("isUsingRightHand", true);
            HandleLightMeleeAction(playerInventory.rightWeapon);
        }
    }
    private void PerformHeavyMeleeAction()
    {
        if (playerManager.isInteracting)
            return;
        if (playerManager.canDoCombo)
            return;

        animatorHandler.anim.SetBool("isUsingRightHand", true);
        HandleHeavyMeleeAttack(playerInventory.rightWeapon);
    }

    public void PerformLightMagicAction(WeaponItem weapon)
    {
        if (playerManager.isInteracting)
            return;

        if (weapon.isFaithCaster)
        {
            if (playerInventory.currentSpell != null && playerInventory.currentSpell.isFaithSpell)
            {
                if(playerStats.currentFocusPoints >= playerInventory.currentSpell.focusPointCost)
                {
                    playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats);
                }
                else
                {
                    animatorHandler.PlayTargetAnimation("Shrug", true);
                }
                
            }
        }
    }

    public void PerformParryWeaponArt(bool isTwoHanding)
    {
        if (playerManager.isInteracting)
            return;
        if (isTwoHanding)
        {
            //If we are dual wielding perform weapon art for right weapon
        }
        else
        {
            animatorHandler.PlayTargetAnimation(playerInventory.leftWeapon.weapon_art, true);
        }        
    }

    private void SuccessfullyCastSpell()
    {
        playerInventory.currentSpell.SucessfullyCastingSpell(animatorHandler, playerStats);
    }

    private void PerformBlockAction()
    {
        if (playerManager.isInteracting)
            return;
       
        if (playerManager.isBlocking)
            return;

        animatorHandler.PlayTargetAnimation("Block Start", false, true);
        equipmentManager.OpenBlockingCollider();
        playerManager.isBlocking = true;
        
    }

    public void AttemptBackstabOrCounter()
    {
        if (playerStats.currentStamina <= 0)
            return;

        RaycastHit hit;

        if (Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
        {
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = weaponSlotManager.rightHandDamageCollider;

            if(enemyCharacterManager != null)
            {
                //check for team id.
                
                playerManager.transform.position = enemyCharacterManager.backStabCollider.CriticalDamagerStandPoint.position;
                Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - playerManager.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                playerManager.transform.rotation = targetRotation;

                int criticalDamage = playerInventory.rightWeapon.criticalDamageMultiplier * rightWeapon.currentWeaponDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                animatorHandler.PlayTargetAnimation("Back Stab", true);
                enemyCharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Back Stabbed", true);
                //deal the damage
            }
        }
        else if (Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.7f, counterLayer))
        {
            //check for team id.
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = weaponSlotManager.rightHandDamageCollider;
           
            if(enemyCharacterManager != null && enemyCharacterManager.canBeCountered)
            {
                playerManager.transform.position = enemyCharacterManager.counterCollider.CriticalDamagerStandPoint.position;

                Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - playerManager.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                playerManager.transform.rotation = targetRotation;

                int criticalDamage = playerInventory.rightWeapon.criticalDamageMultiplier * rightWeapon.currentWeaponDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                animatorHandler.PlayTargetAnimation("Counter", true);

                enemyCharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Countered", true);
            }
        }
    }

    #endregion
}