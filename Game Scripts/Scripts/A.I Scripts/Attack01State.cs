using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack01State : State
{
    public CombatStanceState combatStanceState;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;

    public bool willDoComboNextAttack = false;

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        if (enemyManager.isInteracting && enemyManager.canDoCombo == false)
        {
            return this;
        }
        else if (enemyManager.isInteracting && enemyManager.canDoCombo)
        {
            if (willDoComboNextAttack)
            {
                willDoComboNextAttack = false;
                enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            }
        }
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
      
        HandleRotationTowardsTarget(enemyManager);

        if (enemyManager.isPerformingAction) 
        {
            return combatStanceState;
        }

        if(currentAttack != null)
        {
            //if we are too close to perform the current attack, get a new one
            if(distanceFromTarget < currentAttack.minimumDistanceNeededToAttack)
            {
                return this;
            }

            // if we are close enough, attack
            else if (distanceFromTarget < currentAttack.maximumDistanceNeededToAttack)
            {
                //if the enemy is within our viewable angles, attack
                if(viewableAngle <= currentAttack.maximumAttackAngle && viewableAngle >= currentAttack.minimumAttackAngle)
                {
                    if (enemyManager.currentRecoveryTime <= 0 && enemyManager.isPerformingAction == false)
                    {
                        //stop movement
                        enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                        enemyAnimatorManager.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);

                        // play the attack animation and set the "isPerformingAction" bool to true to prevent further attacks
                        enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
                        enemyManager.isPerformingAction = true;
                        RollForComboChance(enemyManager);

                        if (currentAttack.canCombo && willDoComboNextAttack)
                        {
                            currentAttack = currentAttack.comboAction;
                            return this;
                        }
                        else
                        {
                            // change the recovery time
                            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                            currentAttack = null;
                            return combatStanceState;
                        }
                    }
                }
            }
        
        }

        else
        {
            GetNewAttack(enemyManager);
        }
        return combatStanceState;
    }
    
    private void GetNewAttack(EnemyManager enemyManager)
        {
               Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
              float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
              float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);

             int maxScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        maxScore += enemyAttackAction.attackScore;
                    }
                }
            }

              int randomValue = Random.Range(0, maxScore);
             int temporaryScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++)
            {

                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack && distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
                {
                    if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                    {
                        if (currentAttack != null)
                            return;

                        temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore > randomValue)
                        {
                            currentAttack = enemyAttackAction;
                        }
                    }
                }
            }
    }
    private void HandleRotationTowardsTarget(EnemyManager enemyManager)
    {
        //manual
        if (enemyManager.isPerformingAction)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
        //with navmesh
        else
        {
            Vector3 realtiveDirection = transform.InverseTransformDirection(enemyManager.NavMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.enemyRigidbody.velocity;

            enemyManager.NavMeshAgent.enabled = true;
            enemyManager.NavMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyRigidbody.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.NavMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
    }

    private void RollForComboChance(EnemyManager enemyManager)
    {
        float comboChance = Random.Range(0, 100);

        if(enemyManager.allowAIToPerformCombo && comboChance <= enemyManager.comboLikelyhood)
        {
            willDoComboNextAttack = true;
        }
    }
}
