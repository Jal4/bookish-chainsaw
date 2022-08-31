using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFXManager : MonoBehaviour
{
    PlayerStats playerStats;
    WeaponSlotManager weaponSlotManager;
    public GameObject currentVFX;
    public GameObject instantiatedModelFX;
    public int amountToBeHealed;
    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
    }
    public void HealPlayerFromEffect(int healAmount)
    {
        playerStats.HealPlayer(amountToBeHealed);
        GameObject healParticles = Instantiate(currentVFX, playerStats.transform);
        Destroy(instantiatedModelFX.gameObject);
        weaponSlotManager.LoadBothWeaponsOnSlots();
    }
}
