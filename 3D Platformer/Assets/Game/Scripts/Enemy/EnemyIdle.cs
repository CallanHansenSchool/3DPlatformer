using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : StateMachineBehaviour // Manages what the enemy does while in the Idle state
{
    private float timeToWait = 3.0f; // Current time to wait until patrolling to the next point

    private EnemyManager enemyManager = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyManager = animator.GetComponentInParent<EnemyManager>();
        timeToWait = enemyManager.IdleWaitTime;
        enemyManager.Agent.speed = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyManager.CheckIfChase();

        if (timeToWait <= 0)
        {
            animator.SetTrigger(EnemyAnimatorConstants.PATROL);
        }
        else
        {
            timeToWait -= Time.deltaTime;
        }
    }
}
