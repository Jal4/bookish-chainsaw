using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{

    EnemyLocomotionManager enemyLocomotionManager;
    EnemyAnimatorManager enemyAnimatorManager;
    EnemyStats enemyStats;
    public Rigidbody enemyRigidbody;

    public NavMeshAgent NavMeshAgent;

    public float rotationSpeed = 15;
    public float maximumAttackingRange = 1.5f;
    public bool isInteracting;
    
    [Header("AI Combat Settings")]
    public bool allowAIToPerformCombo;
    public float comboLikelyhood;


    public State currentState;
    public CharacterStats currentTarget;

    public bool isPerformingAction;

    [Header("Combat Flags")]
    public bool canDoCombo;

    [Header("A.I Settings")]
    //detection fov
    public float detectionRadius = 20;
    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;

    public float currentRecoveryTime = 0;


    private void Awake()
    {
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        enemyStats = GetComponent<EnemyStats>();
        NavMeshAgent = GetComponentInChildren<NavMeshAgent>();        
        enemyRigidbody = GetComponent<Rigidbody>();
        NavMeshAgent.enabled = false;
    }
    
    private void Start()
    {
        enemyRigidbody.isKinematic = false;
    }

    private void Update()
    {
        HandleRecoveryTimer();
        HandleStateMachine();

        isInteracting = enemyAnimatorManager.anim.GetBool("isInteracting");
        canDoCombo = enemyAnimatorManager.anim.GetBool("canDoCombo");
        enemyAnimatorManager.anim.SetBool("isDead", enemyStats.isDead);
    }
    private void LateUpdate()
    {
        NavMeshAgent.transform.localPosition = Vector3.zero;
        NavMeshAgent.transform.localRotation = Quaternion.identity;
    }

    private void HandleStateMachine()
    {
        if (currentState != null)
        {
            State nextState = currentState.Tick(this, enemyStats, enemyAnimatorManager);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(State state)
    {
        if(!enemyStats.isDead)
        {
            currentState = state;
        }
        else
        {
            return;
        }
        
    } 
    private void HandleRecoveryTimer()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (isPerformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                isPerformingAction = false;
            }
        }
    }
}
