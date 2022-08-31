using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    public int staminaLevel;
    public float maxStamina;
    public float currentStamina;

    public int focusLevel;
    public float maxFocusPoints;
    public float currentFocusPoints;
   
    public bool isDead;
}
