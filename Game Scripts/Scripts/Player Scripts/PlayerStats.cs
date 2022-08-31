using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public FocusPointsBarScript focusPointsBar;

    PlayerAnimatorHandler animHandler;
    PlayerManager playerManager;
    InputHandler inputHandler;

    public float staminaRegenerationRate = 25;
    public float staminaRegenTimer = 2;

    public float focusPointRegenRate = 10;
    public float focusPointRegenTimer = 2;

    private void Awake()
    {
        healthBar = FindObjectOfType<HealthBar>();
        staminaBar = FindObjectOfType<StaminaBar>();
        focusPointsBar = FindObjectOfType<FocusPointsBarScript>();  
        animHandler = GetComponentInChildren<PlayerAnimatorHandler>();
        playerManager = GetComponent<PlayerManager>();
        inputHandler = GetComponent<InputHandler>();  
    }

    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetCurrentHealth(currentHealth);

        maxStamina = SetMaxStaminaFromStaminaLevel();
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        staminaBar.SetCurrentStamina(currentStamina);

        maxFocusPoints = SetMaxFocusPointsFromFocusLevel();
        currentFocusPoints = maxFocusPoints;
        focusPointsBar.SetMaxFP(maxFocusPoints);
        focusPointsBar.SetCurrentFP(currentFocusPoints);
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }
    private float SetMaxStaminaFromStaminaLevel()
    {
        maxStamina = staminaLevel * 10;
        return maxStamina;
    }

    private float SetMaxFocusPointsFromFocusLevel()
    {
        maxFocusPoints = focusLevel * 10;
        return maxFocusPoints;
    }

    public void TakeDamage(int damage, string damageAnimation = "Damage_01")
    {
        if (playerManager.isInvulnerable)
            return;

        if (isDead)
            return;

        currentHealth = currentHealth - damage;

        healthBar.SetCurrentHealth(currentHealth);

        animHandler.PlayTargetAnimation(damageAnimation, true);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animHandler.PlayTargetAnimation("Death_01", true);
            isDead = true;
        }
    }
    public void TakeDamageNoAnimation(int damage)
    {
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }

    public void TakeStaminaDamage(float damage)
    {
        currentStamina = currentStamina - damage;
        staminaBar.SetCurrentStamina(currentStamina);
    }

    public void RegenerateStamina()
    {
        if (!playerManager.isInteracting)
        {
            if (playerManager.isInteracting || playerManager.isSprinting || inputHandler.rollFlag)
            {
                staminaRegenTimer = 10;
            }
            else
            {
                staminaRegenTimer += Time.deltaTime;

                if (currentStamina < maxStamina && staminaRegenTimer > 2f)
                {
                    currentStamina += staminaRegenerationRate * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }
        }
    }
    public void RegenerateFocusPoints()
    {
        if (!playerManager.isInteracting)
        {
            if (playerManager.isInteracting)
            {
                focusPointRegenTimer = 0;
            }
            else
            {
                focusPointRegenTimer += Time.deltaTime;

                if (currentFocusPoints < maxFocusPoints && focusPointRegenTimer > 2f)
                {
                    currentFocusPoints += focusPointRegenRate * Time.deltaTime;
                    focusPointsBar.SetCurrentFP(Mathf.RoundToInt(currentFocusPoints));
                }
            }
        }
    }
    public void HealPlayer(int healAmount)
    {
        currentHealth = currentHealth + healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.SetCurrentHealth(currentHealth);
    }

    public void DeductFocusPoints(int focusPoints)
    {
        currentFocusPoints = currentFocusPoints - focusPoints;
        
        if(focusPoints < 0)
        {
            currentFocusPoints = 0;
        }
        focusPointsBar.SetCurrentFP(currentFocusPoints);
    }

}
