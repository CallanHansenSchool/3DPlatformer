using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faceplant : StateMachineBehaviour
{
    [SerializeField] private float TotalTimeToFaceplant = 0.8f;
    private float timeToFaceplant = 0.8f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeToFaceplant = TotalTimeToFaceplant;
        animator.gameObject.GetComponentInParent<PlayerMovement>().enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(timeToFaceplant > 0)
        {
            timeToFaceplant -= Time.deltaTime;
        } else
        {
            animator.SetTrigger(PlayerAnimationConstants.FACE_PLANT);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponentInParent<PlayerMovement>().enabled = true;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
