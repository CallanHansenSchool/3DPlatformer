using UnityEngine;

public class EnemyPatrol : StateMachineBehaviour // Manages what the enemy does while in the Patrol state
{
    public int destinationPoint = 0;
    private EnemyManager enemyManager = null;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyManager = animator.GetComponentInParent<EnemyManager>();

        enemyManager.Agent.speed = enemyManager.PatrolSpeed;
        GotoNextPoint();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyManager.CheckIfChase();

        if (!enemyManager.Agent.pathPending && enemyManager.Agent.remainingDistance < 0.5f) // The agent has reached the wanted patrol point
        {
            animator.SetTrigger(EnemyAnimatorConstants.IDLE);
            // Debug.Log("Reached wanted location!");
        }
    }

    void GotoNextPoint()
    {
        if (enemyManager.PatrolPoints.Length == 0)
        {
            return;
        }

        enemyManager.Agent.destination = enemyManager.PatrolPoints[destinationPoint].position;

        // Choose the next point in the array as the destination
        // Cycle back to the start if needed
        destinationPoint = (destinationPoint + 1) % enemyManager.PatrolPoints.Length;
    }
}
