using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderClimbing : MonoBehaviour
{
    [SerializeField] private float climbSpeed = 1.5f;
    [SerializeField] private float jumpOffAmount = 1.5f;

    private CharacterController controller;

    public bool CanClimb = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(Debugger.Instance.Debug)
        {
            Debugger.Instance.Climbing = CanClimb;
            Debugger.Instance.UpdateClimbText();
        }
        
        if (CanClimb)
        {
            PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.CLIMBING, true);

            PlayerManager.Instance.PlayerMovement.enabled = false;

            float verticalInput = Input.GetAxis("Vertical");

            if (Mathf.Abs(verticalInput) > 0.1f)
            {
                PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.CLIMB_MOVE, true);
            } else
            {
                PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.CLIMB_MOVE, false);
            }

            Vector3 _movementVector = new Vector3(0, verticalInput * climbSpeed, 0);

            controller.Move(_movementVector * Time.deltaTime);

            if (PlayerManager.Instance.PlayerMovement.Grounded())
            {
                if (verticalInput < 0)
                {
                    PlayerManager.Instance.PlayerMovement.enabled = true;
                    CanClimb = false;
                }
            }    
        } 
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(GameManager.CLIMBABLE_TAG))
        {
            PlayerManager.Instance.PlayerMovement.enabled = false;
            CanClimb = true;
            transform.rotation = Quaternion.Euler(0f, other.transform.eulerAngles.y, 0f);
            // Debug.Log(other.transform.eulerAngles.y);           
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag(GameManager.CLIMBABLE_TAG))
        {
            PlayerManager.Instance.PlayerMovement.enabled = true;
            CanClimb = false;
            PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.CLIMBING, false);
        }
    }
}
