using UnityEngine;

// Handles the player movement
public class PlayerMovement : MonoBehaviour
{
    #region Constants

    private const float GROUND_CHECK_RADIUS_GROUND = 0.09f;
    private const float JUMP_PRESSED_REMEMBER_TIME = 0.2f;
    private const float GROUNDED_REMEMBER_TIME = 0.2f;
    private const float FALLING_TIME_BEFORE_NO_DOUBLE_JUMP = 5f;

    public const KeyCode JUMP_KEY = KeyCode.Space;
    public const KeyCode CONTROLLER_JUMP_KEY = KeyCode.JoystickButton1;

    #endregion

    #region Movement Values

    [Header("Movement Values")]

    [SerializeField] private float defaultMovementSpeed = 10; // The players default movement speed
    [SerializeField] private float movementSpeed = 0; // How fast the player moves 
    [SerializeField] private float jumpVelocity = 3.5f; // How high the player jumps
 
    [SerializeField] private float rotateSmoothTime = 0.1f; // How slowly the player takes to rotate in the correct direction
    [SerializeField] private float jumpHeightCut = 0.5f; // For more responsive jumping when jumping off a platform and the player doesn't press the jump button while still technically grounded

    #endregion

    #region Gravity

    [Header("Gravity")]

    public float GravityScale = 9.81f; // How fast the player falls
    [SerializeField] private float defaultGravityScale = 2f; // How fast the player falls by default
    [SerializeField] private float slopeGravityScale = 12f; // How fast the player gets pulled down when on a slope
    public float BodySlamGravityScale = 3f; // How fast the player gets pulled down when body slamming

    #endregion

    #region Slopes

    [Header("Slopes")]

    [SerializeField] private float slopeCheckRaycastAmount = 0.5f;
    [SerializeField] private float steepSlopeRaycastCheckAmount = 0.27f;
    [SerializeField] private float steepSlopeAngle = 0.7f;

    private float groundCheckRadius = 0.1f;

    #endregion

    #region References

    [Header("References")]

    [SerializeField] private Transform groundChecker = null; // The position from which the ground is checked from
    [SerializeField] private GameObject playerModel = null;
    [SerializeField] private LayerMask groundLayer;

    private CharacterController controller = null;

    #endregion

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 jumpDirection = Vector3.zero;

    private float turnSmoothVelocity = 0;

    private float jumpPressedRemember = 0; // For more responsive jumping when about to land
    private float groundedRemember = 0; // For more responsive jumping when jumping off a platform and the player doesn't press the jump button while still technically grounded

    public bool CanDoubleJump = false;

    private bool OnEdgeGrounded = false;
    public bool TouchingOverallGround = false;

    [SerializeField] private float amountBeforeFall = -0.5f;

    public bool CanControlPlayer = true;

    private float horizontalInput;
    private float verticalInput;

    private bool checkGround = true;
    private const float TIME_UNTIL_NEXT_CHECK = 1f;
    private float timeUntilNextCheck;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        groundCheckRadius = GROUND_CHECK_RADIUS_GROUND;
        ResetMovementValues();
    }

    void Update()
    {
        if(!CanControlPlayer)
        {
            movementSpeed = 0;
        }

        // Getting user input WASD or arrow keys
        if (!GameManager.Instance.UsingController)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }

        PlayerManager.Instance.Anim.SetFloat(PlayerAnimationConstants.HORIZONTAL_INPUT, Mathf.Abs(horizontalInput));
        PlayerManager.Instance.Anim.SetFloat(PlayerAnimationConstants.VERTICAL_INPUT, Mathf.Abs(verticalInput));

        if (OnSlope() && (horizontalInput > 0 || verticalInput > 0)) // Player is moving on a slope
        {
            GravityScale = slopeGravityScale; // For keeping the player planted on the slope
        }
        else if (Grounded())
        {
            GravityScale = defaultGravityScale;
        }

        if (Grounded())
        {
            if (checkGround)
            {
                TouchingOverallGround = true;
            }
        }
        else if (OnSlope())
        {
            if(checkGround)
            {
                TouchingOverallGround = true;
            }       
        }
        else if (OnEdgeGrounded)
        {
            if(checkGround)
            {
                TouchingOverallGround = true;
            }         
        }
        else
        {
            TouchingOverallGround = false;
        }

        if (TouchingOverallGround)
        {
            PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.GROUNDED, true);
        }
        else
        {
            PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.GROUNDED, false);
        }

        moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        moveDirection.Normalize(); // Normalized to stop faster diagonal speeds

        if (moveDirection.magnitude >= 0.1f) // Is the player pressing any of the movement keys?
        {
            if (CanControlPlayer)
            {
                float _targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + GameManager.Instance.MainCamera.transform.eulerAngles.y;
                float _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref turnSmoothVelocity, rotateSmoothTime);
                transform.rotation = Quaternion.Euler(0f, _angle, 0f); // Apply the float values to smoothly rotate the player in the correct direction

                Vector3 _moveDir = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;
                _moveDir.Normalize();

                controller.Move(_moveDir * movementSpeed * Time.deltaTime); // Applying the values to move the player

            }
        }

        jumpPressedRemember -= Time.deltaTime;
        groundedRemember -= Time.deltaTime;

        if (TouchingOverallGround)
        {
            groundedRemember = GROUNDED_REMEMBER_TIME;
            jumpDirection.y = 0;
            CanDoubleJump = false;
        }
        else
        {
            if (jumpDirection.y < amountBeforeFall)
            {
                PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.FALLING, true);
            }
            else
            {
                PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.FALLING, false);
            }
        }

        if(CanControlPlayer)
        {
            if (Input.GetKeyDown(JUMP_KEY) || Input.GetKeyDown(CONTROLLER_JUMP_KEY))
            {
                jumpPressedRemember = JUMP_PRESSED_REMEMBER_TIME;
                GravityScale = defaultGravityScale;

                if (CanDoubleJump)
                {                   
                    Jump();
                    CanDoubleJump = false;
                    AudioManager.Instance.PlayAudio("DoubleJump");
                }
            }

            if (jumpPressedRemember > 0 && groundedRemember > 0)
            {
                jumpPressedRemember = 0;
                groundedRemember = 0;

                if (!CanDoubleJump)
                {
                    Jump();
                    CanDoubleJump = true;
                    AudioManager.Instance.PlayAudio("Jump");
                }
            }

            if (jumpDirection.y < -FALLING_TIME_BEFORE_NO_DOUBLE_JUMP) // Don't allow the player to double jump if their y velocity is lower than a certain value
            {
                CanDoubleJump = false;
            }

            if (Input.GetKeyUp(JUMP_KEY) || Input.GetKeyUp(CONTROLLER_JUMP_KEY))
            {
                if (jumpDirection.y > 0)
                {
                    jumpDirection.y = jumpDirection.y * jumpHeightCut;
                }
            }
        }       

        jumpDirection.y = jumpDirection.y + (Physics.gravity.y * GravityScale * Time.deltaTime);
    
        controller.Move(jumpDirection * Time.deltaTime);
        
        if (controller.velocity.y == 0 && !Grounded() && !OnSlope()) // Checks if the player is on the very edge of a platform
        {
            OnEdgeGrounded = true;
        }
        else
        {
            OnEdgeGrounded = false;
        }

        PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.TEETER, OnEdgeGrounded);

        CheckSteepSlope();

        if (!checkGround)
        {      
            timeUntilNextCheck -= Time.deltaTime;

            if(timeUntilNextCheck < 0)
            {
                checkGround = true;
            }
        }
    }

    public void ResetMovementValues()
    {
        movementSpeed = defaultMovementSpeed;
        GravityScale = defaultGravityScale;
    }

    void Jump()
    {
        checkGround = false;
        timeUntilNextCheck = TIME_UNTIL_NEXT_CHECK;
        jumpDirection.y = jumpVelocity;
        PlayerManager.Instance.Anim.SetTrigger(PlayerAnimationConstants.JUMP);
    }

    void CheckSteepSlope()
    {
        RaycastHit hit;

        if (Physics.Raycast(groundChecker.transform.position, Vector3.down, out hit, steepSlopeRaycastCheckAmount, groundLayer))
        {
            if (hit.normal != Vector3.up)
            {
                if (hit.normal.y < steepSlopeAngle) // On a steep slope
                {
                    PlayerManager.Instance.PlayerSlopeSlide.Sliding = true;   
                }
            }
        }
    }

    public bool OnSlope() // Detects if the player is on a slope
    {
        RaycastHit hit;

        if (checkGround)
        {
            if (Physics.Raycast(groundChecker.transform.position, Vector3.down, out hit, slopeCheckRaycastAmount, groundLayer))
            {          
                if (hit.normal != Vector3.up)
                {           
                    return true;
                }
                else
                {
                    Debugger.Instance.UpdateSlopeDebugText("No slope");
                    PlayerManager.Instance.PlayerSlopeSlide.Sliding = false;
                    return false;
                }            
            }
        }
        return false;
    }

    public bool Grounded() // Is the player on the ground?
    {      
        Debugger.Instance.ShowGroundChecker(groundChecker.transform.position, groundCheckRadius);   
        return Physics.CheckSphere(groundChecker.transform.position, groundCheckRadius, groundLayer); // Create a small invisible sphere under the player and check if its overlapping with the ground
    }
}
