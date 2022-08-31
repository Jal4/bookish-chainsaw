using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Items")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("Damage")]
    public int baseDamage = 25;
    public int criticalDamageMultiplier = 4;

    [Header("Absorption")]
    public float physicalDamageAbsorption;

    [Header("Idle Animations")]

    public string Right_Hand_Idle;
    public string Left_Hand_Idle;
    public string Two_Handed_Idle;

    [Header("One Handed Attack Animations")]
    public string OH_Light_Attack_01;
    public string OH_Light_Attack_02;
    public string OH_Heavy_Attack_01;
    public string OH_Heavy_Attack_02;

    [Header("Two Handed Light Attack Animations")]

    public string TH_Light_Attack_01;
    public string TH_Light_Attack_02;
    public string TH_Heavy_Attack_01;
    public string TH_Heavy_Attack_02;

    [Header("Weapon Art")]
    public string weapon_art;

    [Header("Stamina Drain")]
    public int baseStamina;
    public float lightAttackMultiplier;
    public float heavyAttackMultiplier;

    [Header("Weapon Type")]
    public bool isSpellCaster;
    public bool isFaithCaster;
    public bool isPyroCaster;
    public bool isMeleeWeapon;
    public bool isShieldWeapon;
}
