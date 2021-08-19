using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour // For enemy AI states to access
{
    public Transform[] PatrolPoints = null;
    public Transform RetreatPoint = null;

    [Header("Movement Speeds")]
    public float PatrolSpeed = 5.0f;
    public float ChaseSpeed = 7.5f;
    public float RetreatSpeed = 4.5f;
    public float AttackingMovementSpeed = 2.0f;

    [Header("Distances")]
    public float ChaseDistance = 4.0f;
    public float StopChaseDistance = 7.0f;
    public float AttackDistance = 3.0f;

    [Header("Attacking")]
    public float AttackSpeed = 1.0f;
    public float LightAttackStrength = 1.0f;
    public float HeavyAttackStrength = 2.0f;

    [Header("Other")]
    public float IdleWaitTime = 3.0f; // The total amount of time it takes for the enemy to wait until patrolling to the next point

    public Animator Anim = null;
    public NavMeshAgent Agent = null;
    private EnemyKnockback enemyKnockback;

    [SerializeField] private float healthRetreatAmount = 0.3f;

    void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        Anim = GetComponentInChildren<Animator>();
        enemyKnockback = GetComponent<EnemyKnockback>();
    }

    void Update()
    {
        CheckIfRetreat();
    }

    void CheckIfRetreat()
    {
        if(GetComponent<EnemyHealth>().CurrentHealth < healthRetreatAmount)
        {
            Anim.SetBool(EnemyAnimatorConstants.RETREAT, true);
        }
    }

    public void CheckIfChase()
    {
        if (Vector3.Distance(PlayerManager.Instance.gameObject.transform.position, gameObject.transform.position) < ChaseDistance)
        {
            Anim.SetTrigger(EnemyAnimatorConstants.CHASE);
        }
    }
}
