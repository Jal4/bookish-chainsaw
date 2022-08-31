using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    InputHandler inputHandler;
    Animator anim;
    CameraHandler cameraHandler;
    PlayerLocomotion playerLocomotion;
    PlayerStats playerStats;
    PlayerAnimatorHandler playerAnimatorHandler;

    InteractableUI interactableUI;
    public GameObject interactableUIGameObject;
    public GameObject itemInteractableGameObject;

    public bool isInteracting;
    [Header("Player Flags")]
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;
    public bool canDoCombo;
    public bool isUsingRightHand;
    public bool isUsingLeftHand;
    public bool isInvulnerable;

    

    private void Awake()
    {
        cameraHandler = FindObjectOfType<CameraHandler>();
        backStabCollider = GetComponentInChildren<CriticalDamageCollider>();
        playerAnimatorHandler = GetComponentInChildren<PlayerAnimatorHandler>();
        inputHandler = GetComponent<InputHandler>();
        anim = GetComponentInChildren<Animator>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        interactableUI = FindObjectOfType<InteractableUI>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Update()
    {
        float delta = Time.deltaTime;

        isInteracting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");
        isUsingRightHand = anim.GetBool("isUsingRightHand");
        isUsingLeftHand = anim.GetBool("isUsingLeftHand");
        isInvulnerable = anim.GetBool("isInvulnerable");
        anim.SetBool("isBlocking", isBlocking);
        anim.SetBool("isInAir", isInAir);
        anim.SetBool("isDead", playerStats.isDead);
               
        inputHandler.rollFlag = false;
        inputHandler.sprintFlag = false;

        isSprinting = inputHandler.b_Input; 
        inputHandler.TickInput(delta);
        playerAnimatorHandler.canRotate = anim.GetBool("canRotate");
        playerLocomotion.HandleRollingAndSprinting(delta);
        CheckForInteractableObject();
        playerLocomotion.HandleJumping();
        playerStats.RegenerateStamina();
        playerStats.RegenerateFocusPoints();
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;
        playerLocomotion.HandleMovement(delta);
        playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
        playerLocomotion.HandleRotation(delta);
    }

    private void LateUpdate()
    {
        inputHandler.rollFlag = false;
        inputHandler.heavyAttack_Input = false;
        inputHandler.lightAttack_Input = false;
        inputHandler.parry_Input = false;
        inputHandler.d_PadRight = false;
        inputHandler.d_PadLeft = false;
        inputHandler.d_PadUp = false;
        inputHandler.d_PadDown = false;
        inputHandler.a_Input = false;
        inputHandler.jump_Input = false;
        inputHandler.inventory_Input = false;

        float delta = Time.deltaTime;
        if (cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
        }

        if (isInAir)
        {
            playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
        }
    }
    public void CheckForInteractableObject()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                if (interactableObject != null)
                {
                    string interactableText = interactableObject.interactableText;
                    interactableUI.interactableText.text = interactableText;
                    interactableUIGameObject.SetActive(true);

                    if (inputHandler.a_Input)
                    {
                        hit.collider.GetComponent<Interactable>().Interact(this);
                    }
                }
            }
        }
        else
        {
            if(interactableUIGameObject != null)
            {
                interactableUIGameObject.SetActive(false);
            }

            if(itemInteractableGameObject != null && inputHandler.a_Input)
            {
                itemInteractableGameObject.SetActive(false);
            }
        }
    }
}
