using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Spells/Healing Spell")]
public class HealingSpell : SpellItem
{
    public int healAmount;

    public override void AttemptToCastSpell(PlayerAnimatorHandler animatorHandler, PlayerStats playerStats)
    {
        base.AttemptToCastSpell(animatorHandler, playerStats);
        GameObject instantiatedWarmUpSpellFX = Instantiate(spellWarmupFX, animatorHandler.transform);
        animatorHandler.PlayTargetAnimation(spellAnimation, true);
        Debug.Log("attempting to cast");
    }

    public override void SucessfullyCastingSpell(PlayerAnimatorHandler animatorHandler, PlayerStats playerStats)
    {
        base.SucessfullyCastingSpell(animatorHandler, playerStats);
        GameObject instantiateSpellFX = Instantiate(spellCastFX, animatorHandler.transform);
        playerStats.HealPlayer(healAmount);
        Debug.Log("Successful casting");
    }
}
