using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    public bool b_Input;
    public bool a_Input;
    public bool consumable_Input;
    public bool y_Input;
    public bool lightAttack_Input;
    public bool heavyAttack_Input;
    public bool parry_Input;
    public bool block_Input;
    public bool critical_Attack_Input;
    public bool lockOnInput;
    public bool lockOnCycleRight_Input;
    public bool lockOnCycleLeft_Input;

    public bool jump_Input;
    
    public bool d_PadUp;
    public bool d_PadDown;
    public bool d_PadLeft;
    public bool d_PadRight;
    public bool inventory_Input;

    public bool rollFlag;
    public bool twoHandFlag;
    public bool sprintFlag;
    public bool comboFlag;
    public bool lockOnFlag;
    public bool inventoryFlag;
    public float rollInputTimer;

    public Transform criticalAttackRayCastStartPoint;

    PlayerFXManager playerFXManager;
    PlayerControls inputActions;
    PlayerAttacker playerAttacker;
    PlayerInventory playerInventory;
    PlayerManager playerManager;
    BlockingCollider blockingCollider;
    UIManager uiManager;
    CameraHandler cameraHandler;
    WeaponSlotManager weaponSlotManager;
    PlayerAnimatorHandler animatorHandler;
    PlayerStats playerStats;

    Vector2 movementInput;
    Vector2 cameraInput;

    private void Awake()
    {
        playerAttacker = GetComponentInChildren<PlayerAttacker>();
        playerInventory = GetComponent<PlayerInventory>();
        playerManager = GetComponent<PlayerManager>();
        playerStats = GetComponent<PlayerStats>();
        playerFXManager = GetComponentInChildren<PlayerFXManager>();
        blockingCollider = GetComponentInChildren<BlockingCollider>();
        uiManager = FindObjectOfType<UIManager>();
        cameraHandler = FindObjectOfType<CameraHandler>();
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        animatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
    }

    private void OnEnable()
    {
        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            inputActions.PlayerActions.LightAttack.performed += i => lightAttack_Input = true;
            inputActions.PlayerActions.HeavyAttack.performed += i => heavyAttack_Input = true;
            inputActions.PlayerActions.Parry.performed += i => parry_Input = true;
            inputActions.PlayerActions.Block.performed += i => block_Input = true;
            inputActions.PlayerActions.Block.canceled += i => block_Input = false;
            inputActions.PlayerActions.Roll.performed += i => b_Input = true;
            inputActions.PlayerActions.Roll.canceled += i => b_Input = false;
            inputActions.PlayerActions.Consumable.performed += i =>  consumable_Input = true;
            inputActions.PlayerQuickSlots.DPadRight.performed += i => d_PadRight = true;
            inputActions.PlayerQuickSlots.DPadLeft.performed += i => d_PadLeft = true;
            inputActions.PlayerActions.Interact.performed += i => a_Input = true;
            inputActions.PlayerActions.Jump.performed += i => jump_Input = true;
            inputActions.PlayerActions.Inventory.performed += i => inventory_Input = true;
            inputActions.PlayerActions.LockOnHandler.performed += i => lockOnInput = true;
            inputActions.PlayerMovement.LockOnTargetRight.performed += i => lockOnCycleRight_Input = true;
            inputActions.PlayerMovement.LockOnTargetLeft.performed += i => lockOnCycleLeft_Input = true;
            inputActions.PlayerActions.ToggleTwoHanded.performed += i => y_Input = true;
            inputActions.PlayerActions.CriticalAttack.performed += i => critical_Attack_Input = true;
        }

        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        HandleMoveInput(delta);
        HandleRollInput(delta);
        HandleCombatInput(delta);
        HandleQuickSlotsInput();
        HandleInventoryInput();
        HandleLockOnInput();
        HandleTwoHandInput();
        HandleCriticalAttackInput();
        HandleUseConsumableInput();
    }

    private void HandleMoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    private void HandleRollInput(float delta)
    {


        if (b_Input)
        {
            rollInputTimer += delta;

            if(playerStats.currentStamina <= 0)
            {
                b_Input = false;
                sprintFlag = false;
            }

            if(moveAmount > 0.5f && playerStats.currentStamina > 0)
            {
                sprintFlag = true;
            }
        }
        else
        {
            sprintFlag = false;
           
            if (rollInputTimer > 0 && rollInputTimer < 0.5f)
            {
                 rollFlag = true;
            }

            rollInputTimer = 0;
        }
    }

    private void HandleCombatInput(float delta)
    {
        if(lightAttack_Input)
        {
            playerAttacker.HandleLightAction();
        }

        if(heavyAttack_Input)
        {
            playerAttacker.HandleHeavyAction();
        }

        if(block_Input)
        {
            playerAttacker.HandleBlockAction();
        }
        else
        {
            playerManager.isBlocking = false;

            if (blockingCollider.blockingCollider.enabled)
            {
                blockingCollider.DisableBlockingCollider();
            }
        }

        if (parry_Input)
        {
            if(twoHandFlag)
            {
                // if two handed handle Weapon Art
            }
            else
            {
                playerAttacker.HandleParryAction();
            }
        }
    }

    private void HandleQuickSlotsInput()
    {

        if (d_PadRight)
        {
            playerInventory.ChangeRightWeapon();
        }
        else if(d_PadLeft)
        {
            playerInventory.ChangeLeftWeapon();
        }
    }

    public void HandleInventoryInput()
    {

        if (inventory_Input)
        {
            inventoryFlag = !inventoryFlag;

            if (inventoryFlag)
            {
                uiManager.OpenSelectWindow();
                uiManager.updateUI();
                uiManager.hudWindow.SetActive(false);
            }

            else
            {
                uiManager.CloseSelectWindow();
                uiManager.CloseAllInventoryWindows();
                uiManager.hudWindow.SetActive(true);
            }
        }
    }

    private void HandleLockOnInput()
    {
        if (lockOnInput && lockOnFlag == false)
        {
            lockOnInput = false;
            cameraHandler.HandleLockOn();
            
            if(cameraHandler.nearestLockOnTarget != null)
            {
                cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                lockOnFlag = true;
            }
        }
        
        else if (lockOnInput && lockOnFlag)
        {
            lockOnInput = false;
            lockOnFlag = false;
            cameraHandler.ClearLockOnTargets();
        }

        if (lockOnFlag && lockOnCycleLeft_Input)
        {
            lockOnCycleLeft_Input = false;
            cameraHandler.HandleLockOn();
            if(cameraHandler.leftLockTarget != null)
            {
                cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
            }
        }

        else if (lockOnFlag && lockOnCycleRight_Input)
        {
            lockOnCycleRight_Input = false;
            cameraHandler.HandleLockOn();
            if (cameraHandler.RightLockTarget != null)
            {
                cameraHandler.currentLockOnTarget = cameraHandler.RightLockTarget;
            }
        }

        cameraHandler.SetCameraHeight();
    }

    private void HandleTwoHandInput()
    {
        if(y_Input)
        {
            y_Input = false;

            twoHandFlag = !twoHandFlag;

            if (twoHandFlag)
            {
                weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
            }
            else
            {
                weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
                weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon, true);
            }
        }
    }
    private void HandleCriticalAttackInput()
    {
        if(critical_Attack_Input)
        {
            critical_Attack_Input = false;
            playerAttacker.AttemptBackstabOrCounter();
        }
    }

    private void HandleUseConsumableInput()
    {
        if(consumable_Input)
        {
            consumable_Input = false;
            playerInventory.currentConsumable.AttemptToConsumeTheItem(animatorHandler, weaponSlotManager, playerFXManager);
        }
    }
}
