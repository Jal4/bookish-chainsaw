using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellItem : Item
{
    public GameObject spellWarmupFX;
    public GameObject spellCastFX;

    public string spellAnimation;

    [Header("Spell Cost")]
    public int focusPointCost;

    [Header("Spell Type")]
    public bool isFaithSpell;
    public bool isSorcerrySpell;
    public bool isPyroSpell;

    [Header("Spell Description")]
    [TextArea]
    public string spellDescription;

    public virtual void AttemptToCastSpell(PlayerAnimatorHandler animatorHandler, PlayerStats playerStats)
    {
        Debug.Log("You are trying to cast a spell");
    }

    public virtual void SucessfullyCastingSpell(PlayerAnimatorHandler animatorHandler, PlayerStats playerStats)
    {
        Debug.Log("You casted the spell");
        playerStats.DeductFocusPoints(focusPointCost);
    }
}
