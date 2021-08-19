using UnityEngine;

public class EnemyPatrol : StateMachineBehaviour // Manages what the enemy does while in the Patrol state
{
    public int destinationPoint = -1;
    private EnemyManager enemyManager = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyManager = animator.GetComponentInParent<EnemyManager>();
        enemyManager.Agent.enabled = true;
        enemyManager.Agent.speed = enemyManager.PatrolSpeed;
        GotoNextPoint();
    }

    void GotoNextPoint()
    {
        if(enemyManager.PatrolPoints.Length > 0) 
        {
            // Choose the next point in the array as the destination
            // Cycle back to the start when needed
            destinationPoint = (destinationPoint + 1) % enemyManager.PatrolPoints.Length;

            enemyManager.Agent.SetDestination(enemyManager.PatrolPoints[destinationPoint].position);
        }  
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyManager.CheckIfChase();

        if (!enemyManager.Agent.pathPending && enemyManager.Agent.remainingDistance < 0.05f) // The agent has reached the wanted patrol point
        {
            animator.SetTrigger(EnemyAnimatorConstants.IDLE);
            // Debug.Log("Reached wanted location!");
        }
    }
}
