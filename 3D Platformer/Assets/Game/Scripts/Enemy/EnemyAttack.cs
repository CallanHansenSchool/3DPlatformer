using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : StateMachineBehaviour // Manages what the enemy does while in the Attack state
{
    private EnemyManager enemyManager = null;

    private float timeSinceLastAttack = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyManager = animator.GetComponentInParent<EnemyManager>();
        enemyManager.Agent.speed = enemyManager.AttackingMovementSpeed;
        timeSinceLastAttack = enemyManager.AttackSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector3.Distance(PlayerManager.Instance.gameObject.transform.position, animator.transform.parent.position) > enemyManager.AttackDistance)
        {
            animator.SetTrigger(EnemyAnimatorConstants.CHASE);
        } else
        {
            if(timeSinceLastAttack < 0)
            {
                if(!PlayerManager.Instance.Dead) // Make sure that the player isnt dead
                {
                    PlayerHealth.Instance.TakeDamage(enemyManager.AttackStrength);
                    timeSinceLastAttack = enemyManager.AttackSpeed;
                }           
            } else
            {
                timeSinceLastAttack -= Time.deltaTime;
            }
        }
    }
}
