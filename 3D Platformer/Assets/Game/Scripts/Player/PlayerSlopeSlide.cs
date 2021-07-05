using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlopeSlide : MonoBehaviour
{
    private Rigidbody rb;

    public bool Sliding = false;
    private bool addedRB = false;

    void Update()
    {
        if(Sliding)
        {
            Slide();
        } else
        {
            if (addedRB)
            {
                PlayerManager.Instance.PlayerMovement.CanControlPlayer = true;
                Destroy(rb);
                GetComponent<CharacterController>().enabled = true;
                GetComponent<CapsuleCollider>().enabled = false;
                PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.SLIDING, false);
                PlayerManager.Instance.PlayerMovement.ResetMovementValues();
                addedRB = false;
            }
        }
    }

    void Slide()
    {
        if (!addedRB)
        {
            PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.SLIDING, true);
            rb = gameObject.AddComponent<Rigidbody>();
            addedRB = true;

            GetComponent<CharacterController>().enabled = false;
            PlayerManager.Instance.PlayerMovement.CanControlPlayer = false;
          
            GetComponent<CapsuleCollider>().enabled = true;
        }
        else
        {
            return;
        }
    }
}