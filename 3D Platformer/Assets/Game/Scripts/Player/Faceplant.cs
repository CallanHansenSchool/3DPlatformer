using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faceplant : StateMachineBehaviour
{
    [SerializeField] private float TotalTimeToFaceplant = 0.8f;
    private float timeToFaceplant = 0.8f;

    [SerializeField] private float facePlantDamage = 1.5f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeToFaceplant = TotalTimeToFaceplant;
        animator.gameObject.GetComponentInParent<PlayerMovement>().CanControlPlayer = false;
        PlayerHealth.Instance.CurrentHealth -= facePlantDamage;
        HUD.Instance.UpdateHUD();
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
}
