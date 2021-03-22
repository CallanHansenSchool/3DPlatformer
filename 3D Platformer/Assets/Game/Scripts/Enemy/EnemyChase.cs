using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : StateMachineBehaviour // Manages what the enemy does while in the Chase state
{
    private EnemyManager enemyManager = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyManager = animator.GetComponentInParent<EnemyManager>();       
        enemyManager.Agent.speed = enemyManager.ChaseSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyManager.Agent.destination = PlayerManager.Instance.transform.position;

        if (Vector3.Distance(PlayerManager.Instance.gameObject.transform.position, animator.transform.parent.position) > enemyManager.StopChaseDistance)
        {
            animator.SetTrigger(EnemyAnimatorConstants.PATROL);
        }
        else if (Vector3.Distance(PlayerManager.Instance.gameObject.transform.position, animator.transform.parent.position) <= enemyManager.AttackDistance)
        {
            animator.SetTrigger(EnemyAnimatorConstants.ATTACK);
        }
    }
}
