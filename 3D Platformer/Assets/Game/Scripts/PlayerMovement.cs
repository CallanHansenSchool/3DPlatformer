using UnityEngine;

// Handles the player movement
public class PlayerMovement : MonoBehaviour
{
    #region Constants

    private const float GROUND_CHECK_RADIUS = 0.11f;
    private const KeyCode JUMP_KEY = KeyCode.Space;

    #endregion

    #region Movement Values

    [Header("Movement Values")]

    [SerializeField] private float movementSpeed = 0; // How fast the player moves 
    [SerializeField] private float jumpVelocity = 3.5f; // How high the player jumps
    [SerializeField] private float gravityScale = 9.81f; // How fast the player falls 
    [SerializeField] private float rotateSmoothTime = 0.1f; // How slowly the player takes to rotate in the correct direction

    #endregion

    #region References

    [Header("References")]

    [SerializeField] private Transform groundChecker; // The position from which the ground is checked from
    [SerializeField] private GameObject playerModel;

    private CharacterController controller = null;

    #endregion

    private Vector3 jumpDirection;
    private float turnSmoothVelocity = 0;

    public LayerMask GroundLayer;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Getting user input WASD or arrow keys
        float _horizontalInput = Input.GetAxis("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");

        Vector3 _direction = new Vector3(_horizontalInput, 0, _verticalInput);

        _direction.Normalize(); // Normalized to stop faster diagonal speeds

        if (_direction.magnitude >= 0.1f) // Is the player pressing any of the movement keys?
        {
            float _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + GameManager.Instance.MainCamera.transform.eulerAngles.y;
            float _angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref turnSmoothVelocity, rotateSmoothTime);
            transform.rotation = Quaternion.Euler(0f, _angle, 0f); // Apply the float values to smoothly rotate the player in the correct direction

            Vector3 _moveDir = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;

            _moveDir.Normalize(); 

            controller.Move(_moveDir * movementSpeed * Time.deltaTime); // Applying the values to move the player
        }

        if (Grounded())
        {
            jumpDirection.y = 0;

            if (Input.GetKeyDown(JUMP_KEY))
            {
                jumpDirection.y = jumpVelocity;
            }
        }

        jumpDirection.y = jumpDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(jumpDirection * Time.deltaTime);
    }

    bool Grounded() // Is the player on the ground?
    {
        return Physics.CheckSphere(groundChecker.transform.position, GROUND_CHECK_RADIUS, GroundLayer); // Create a small invisible sphere under the player and check if its overlapping with the ground
    }
}
