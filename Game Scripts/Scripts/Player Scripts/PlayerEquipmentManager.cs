using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    InputHandler inputHandler;
    PlayerInventory playerInventory;
    
    [Header("Equipment Model Changers")]
    //Head Equipment
    HelmetModelChanger helmetModelChanger;
    //Torso Equipment
    TorsoModelChanger torsoModelChanger;
    LeftUpperArmModelChanger leftUpperArmModelChanger;
    RightUpperArmModelChanger rightUpperArmModelChanger;
    RightShoulderModelChanger rightShoulderModelChanger;
    LeftShoulderModelChanger leftShoulderModelChanger;
    //Hips Equipment
    HipsModelChanger hipsModelChanger;
    LeftLegModelChanger leftLegModelChanger;
    RightLegModelChanger rightLegModelChanger;
    RightKneePadModelChanger rightKneePadModelChanger;
    LeftKneePadModelChanger leftKneePadModelChanger;
    //Hand Equipment
    LeftLowerArmModelChanger leftLowerArmModelChanger;
    LowerRightArmModelChanger rightLowerArmModelChanger;
    RightHandModelChanger rightHandModelChanger;
    LeftHandModelChanger leftHandModelChanger;
    RightElbowModelChanger rightElbowModelChanger;
    LeftElbowModelChanger leftElbowModelChanger;

    [Header("Default Naked Models")]
    public GameObject nakedHeadModel;
    public string nakedTorsoModel;
    public string nakedHipModel;
    public string nakedLeftLegModel;
    public string nakedRightLegModel;
    public string nakedUpperRightArmModel;
    public string nakedUpperLeftArmModel;
    public string nakedLowerLeftArmModel;
    public string nakedLowerRightArmModel;
    public string nakedLeftHandModel;
    public string nakedRightHandModel;    

    public BlockingCollider blockingCollider;

    private void Awake()
    {
        inputHandler = GetComponentInParent<InputHandler>();
        playerInventory = GetComponentInParent<PlayerInventory>();
        helmetModelChanger = GetComponentInChildren<HelmetModelChanger>();
        torsoModelChanger = GetComponentInChildren<TorsoModelChanger>();
        hipsModelChanger = GetComponentInChildren<HipsModelChanger>();
        rightKneePadModelChanger = GetComponentInChildren<RightKneePadModelChanger>();
        leftKneePadModelChanger = GetComponentInChildren<LeftKneePadModelChanger>();
        rightShoulderModelChanger = GetComponentInChildren<RightShoulderModelChanger>();
        leftShoulderModelChanger = GetComponentInChildren<LeftShoulderModelChanger>();
        rightElbowModelChanger = GetComponentInChildren<RightElbowModelChanger>();
        leftElbowModelChanger = GetComponentInChildren<LeftElbowModelChanger>();
        rightLegModelChanger = GetComponentInChildren<RightLegModelChanger>();
        leftLegModelChanger = GetComponentInChildren<LeftLegModelChanger>();
        leftUpperArmModelChanger = GetComponentInChildren<LeftUpperArmModelChanger>();
        rightUpperArmModelChanger = GetComponentInChildren<RightUpperArmModelChanger>();
        leftLowerArmModelChanger = GetComponentInChildren<LeftLowerArmModelChanger>();
        rightLowerArmModelChanger = GetComponentInChildren<LowerRightArmModelChanger>();
        rightHandModelChanger = GetComponentInChildren<RightHandModelChanger>();
        leftHandModelChanger = GetComponentInChildren<LeftHandModelChanger>();
    }

    private void Start()
    {
        EquipAllEquipmentModelsOnStart();
    }

    private void EquipAllEquipmentModelsOnStart()
    {
        //Helmet Equipment
        helmetModelChanger.UnequipAllHelmetModels();

        if (playerInventory.currentHelmetEquipment != null)
        {
            nakedHeadModel.SetActive(false);
            helmetModelChanger.EquipHelmetModelByName(playerInventory.currentHelmetEquipment.helmetModelName);
        }
        else
        {
            nakedHeadModel.SetActive(true);
        }
       
        //Torso Euipment
        torsoModelChanger.UnequipAllTorsoModels();
        rightUpperArmModelChanger.UnequipAllModels();
        leftUpperArmModelChanger.UnequipAllModels();
        rightShoulderModelChanger.UnequipAllModels();
        leftShoulderModelChanger.UnequipAllModels();

        
        if (playerInventory.currentTorsoEquipment != null)
        {
            torsoModelChanger.EquipTorsoModelByName(playerInventory.currentTorsoEquipment.torsoModelName);
            leftUpperArmModelChanger.EquipModelByName(playerInventory.currentTorsoEquipment.leftUpperArmModelName);
            rightUpperArmModelChanger.EquipModelByName(playerInventory.currentTorsoEquipment.rightUpperArmModelName);
            rightShoulderModelChanger.EquipModelByName(playerInventory.currentTorsoEquipment.rightShoulderPadModelName);
            leftShoulderModelChanger.EquipModelByName(playerInventory.currentTorsoEquipment.leftShoulderPadModelName);
        }
        else
        {
            torsoModelChanger.EquipTorsoModelByName(nakedTorsoModel);
            leftUpperArmModelChanger.EquipModelByName(nakedUpperLeftArmModel);
            rightUpperArmModelChanger.EquipModelByName(nakedUpperRightArmModel);
            rightElbowModelChanger.EquipModelByName(null);
            leftElbowModelChanger.EquipModelByName(null);
        }

        //Leg Equipment
        hipsModelChanger.UnequipAllHipsModels();
        leftLegModelChanger.UnequipAllLeftLegModels();
        rightLegModelChanger.UnequipAllRightLegModels();
        leftKneePadModelChanger.UnequipAllModels();
        rightKneePadModelChanger.UnequipAllModels();

        if(playerInventory.currentLegEquipment != null)
        {
            hipsModelChanger.EquipHipModelByName(playerInventory.currentLegEquipment.hipModelName);
            leftLegModelChanger.EquipLeftLegModelByName(playerInventory.currentLegEquipment.leftLegName);
            rightLegModelChanger.EquipRightLegModelByName(playerInventory.currentLegEquipment.rightLegName);
            leftKneePadModelChanger.EquipModelByName(playerInventory.currentLegEquipment.leftKneePadName);
            rightKneePadModelChanger.EquipModelByName(playerInventory.currentLegEquipment.rightKneePadName);
        }
        else
        {
            hipsModelChanger.EquipHipModelByName(nakedHipModel);
            leftLegModelChanger.EquipLeftLegModelByName(nakedLeftLegModel);
            rightLegModelChanger.EquipRightLegModelByName(nakedRightLegModel);
            leftKneePadModelChanger.EquipModelByName(null);
            rightKneePadModelChanger.EquipModelByName(null);
        }
        //Hand Equipment
        leftLowerArmModelChanger.UnequipAllModels();
        leftHandModelChanger.UnequipAllModels();
        rightHandModelChanger.UnequipAllModels();
        rightLowerArmModelChanger.UnequipAllModels();
        leftElbowModelChanger.UnequipAllModels();
        rightElbowModelChanger.UnequipAllModels();

        if (playerInventory.currentHandEquipment != null)
        {
            leftLowerArmModelChanger.EquipModelByName(playerInventory.currentHandEquipment.lowerLeftArmModelName);
            leftHandModelChanger.EquipModelByName(playerInventory.currentHandEquipment.leftHandModelName);
            rightHandModelChanger.EquipModelByName(playerInventory.currentHandEquipment.rightHandModelName);
            rightLowerArmModelChanger.EquipModelByName(playerInventory.currentHandEquipment.lowerRightArmModelName);
            rightElbowModelChanger.EquipModelByName(playerInventory.currentHandEquipment.rightElbowPad);
            leftElbowModelChanger.EquipModelByName(playerInventory.currentHandEquipment.leftElbowPad);            
        }
        else
        {
            leftLowerArmModelChanger.EquipModelByName(nakedLowerLeftArmModel);
            leftHandModelChanger.EquipModelByName(nakedLeftHandModel);
            rightHandModelChanger.EquipModelByName(nakedRightHandModel);
            rightLowerArmModelChanger.EquipModelByName(nakedLowerRightArmModel);
            leftElbowModelChanger.EquipModelByName(null);
            rightElbowModelChanger.EquipModelByName(null);
        }
    }

    public void OpenBlockingCollider()
    {
        if(inputHandler.twoHandFlag)
        {
            blockingCollider.SetColliderDamageAbsorption(playerInventory.rightWeapon);
        }
        else
        {
            blockingCollider.SetColliderDamageAbsorption(playerInventory.leftWeapon);
        }
        blockingCollider.EnableBlockingCollider();
    }
    public void DisableBlockingCollider()
    {
        blockingCollider.DisableBlockingCollider();
    }
}
