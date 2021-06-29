using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour // For enemy AI states to access
{
    public Transform[] PatrolPoints = null;


    [Header("Movement Speeds")]
    public float PatrolSpeed = 5.0f;
    public float ChaseSpeed = 7.5f;
    public float AttackingMovementSpeed = 2.0f;

    [Header("Distances")]
    public float ChaseDistance = 4.0f;
    public float StopChaseDistance = 7.0f;
    public float AttackDistance = 3.0f;

    [Header("Attacking")]
    public float AttackSpeed = 1.0f;
    public float AttackStrength = 1.0f;

    [Header("Other")]
    public float IdleWaitTime = 3.0f; // The total amount of time it takes for the enemy to wait until patrolling to the next point

    public Animator Anim = null;
    public NavMeshAgent Agent = null;
    private EnemyKnockback enemyKnockback;

    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponentInChildren<Animator>();
        enemyKnockback = GetComponent<EnemyKnockback>();
    }

    public void CheckIfChase()
    {
        if (Vector3.Distance(PlayerManager.Instance.gameObject.transform.position, gameObject.transform.position) < ChaseDistance)
        {
            if(!enemyKnockback.KnockingBack)
            {
                Anim.SetTrigger(EnemyAnimatorConstants.CHASE);
            }          
        }
    }
}
