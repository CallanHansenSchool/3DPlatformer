using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : StateMachineBehaviour // Manages what the enemy does while in the Chase state
{
    private EnemyManager enemyManager = null;
    [SerializeField] private GameObject player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = PlayerManager.Instance.gameObject;
        enemyManager = animator.GetComponentInParent<EnemyManager>();       
        enemyManager.Agent.enabled = true;
        enemyManager.Agent.speed = enemyManager.ChaseSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(player.transform.position, animator.transform.parent.position) > enemyManager.StopChaseDistance)
        {
            animator.SetTrigger(EnemyAnimatorConstants.PATROL);
        }
        else if (Vector3.Distance(player.transform.position, animator.transform.parent.position) < enemyManager.AttackDistance) // Player is in attacking range
        {
            int randomNumber = Random.Range(0, 2);

            // Randomly decide which attack to use on player
            if (randomNumber == 0)
            {
                animator.SetTrigger(EnemyAnimatorConstants.LIGHT_ATTACK);
            }
            else if (randomNumber == 1)
            {
                animator.SetTrigger(EnemyAnimatorConstants.HEAVY_ATTACK);
            }
            else
            {
                Debug.LogError("Enemy did an attack that is unavailable!");
            }
        }
        else
        {
            enemyManager.Agent.destination = player.transform.position;
        }
    }
}
