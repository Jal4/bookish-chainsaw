using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    EnemyAnimatorManager animator;
    
    public UIEnemyHealthBar enemyhealthBar;

    private void Awake()
    {
        animator = GetComponentInChildren<EnemyAnimatorManager>();
    }

    private void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        enemyhealthBar.SetMaxHealth(maxHealth);
    }

    private int SetMaxHealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamageNoAnimation(int damage)
    {
        currentHealth = currentHealth - damage;

        enemyhealthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
        }
    }

    public void TakeDamage(int damage, string damageAnimation = "Damage_01")
    {
        if(isDead) 
            return;

        currentHealth = currentHealth - damage;
        enemyhealthBar.SetHealth(currentHealth);


        animator.PlayTargetAnimation(damageAnimation, true);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.PlayTargetAnimation("Death_01", true);
        }
    }
}
