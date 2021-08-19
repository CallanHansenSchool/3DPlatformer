using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingUp : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerManager.Instance.PlayerMovement.CanControlPlayer = true;
        PlayerManager.Instance.PlayerMovement.ResetMovementValues();
    }
}
