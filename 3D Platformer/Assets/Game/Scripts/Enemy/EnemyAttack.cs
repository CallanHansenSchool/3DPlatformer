using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : StateMachineBehaviour // Manages what the enemy does while in the Attack state
{
    private EnemyManager enemyManager = null;

    private float timeSinceLastAttack = 0;

    private int randomAttack;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyManager = animator.GetComponentInParent<EnemyManager>();
        enemyManager.Agent.enabled = false;
        enemyManager.Agent.speed = enemyManager.AttackingMovementSpeed;
        timeSinceLastAttack = enemyManager.AttackSpeed;
        Attack(animator);    
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(PlayerManager.Instance.gameObject.transform.position, animator.transform.parent.position) > enemyManager.AttackDistance)
        {
            animator.SetTrigger(EnemyAnimatorConstants.CHASE);
        }
        else // The player is in attack range
        {
            Attack(animator);
        }
    }

    void Attack(Animator animator)
    {
        if (timeSinceLastAttack < 0)
        {
            if (!PlayerManager.Instance.Dead) // Make sure that the player isnt dead
            {
                randomAttack = Random.Range(0, 3);

                if (!PlayerManager.Instance.PlayerMelee.Blocking)
                {
                    switch (randomAttack)
                    {
                        case 0:
                            animator.SetTrigger(EnemyAnimatorConstants.LIGHT_ATTACK);
                            //PlayerHealth.Instance.TakeDamage(enemyManager.LightAttackStrength, true);
                            break;

                        case 1:
                            animator.SetTrigger(EnemyAnimatorConstants.HEAVY_ATTACK);
                            //PlayerHealth.Instance.TakeDamage(enemyManager.HeavyAttackStrength, false);
                            break;

                        case 2:
                            animator.SetTrigger(EnemyAnimatorConstants.HEAVY_ATTACK);
                            //PlayerHealth.Instance.TakeDamage(enemyManager.HeavyAttackStrength, false);
                            break;
                    }
                }
                else
                {
                    switch (randomAttack) // Take half the damage if blocking
                    {
                        case 0:
                            //PlayerHealth.Instance.TakeDamage(enemyManager.LightAttackStrength * 0.5f, true);
                            break;

                        case 1:
                            //PlayerHealth.Instance.TakeDamage(enemyManager.HeavyAttackStrength * 0.5f, false);
                            break;

                        case 2:
                            //PlayerHealth.Instance.TakeDamage(enemyManager.HeavyAttackStrength * 0.5f, false);
                            break;
                    }

                    //animator.SetTrigger(EnemyAnimatorConstants.TAKE_DAMAGE);

                
                }



                timeSinceLastAttack = enemyManager.AttackSpeed;

            }
            else
            {
                animator.SetTrigger(EnemyAnimatorConstants.PATROL);
            }
        }
        else
        {
            timeSinceLastAttack -= Time.deltaTime;
        }
    }
}