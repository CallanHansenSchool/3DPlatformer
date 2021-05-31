using UnityEngine;
public class PlayerAnimationConstants : MonoBehaviour// A list of Animator() component constant parameter names
{
    // Input
    public const string HORIZONTAL_INPUT = "HorizontalInput";
    public const string VERTICAL_INPUT = "VerticalInput";

    // Movement
    public const string GROUNDED = "Grounded";
    public const string JUMP = "Jump";
    

    // Climbing
    public const string CLIMBING = "Climbing";
    public const string CLIMB_MOVE = "ClimbMove";

    // Death
    public const string DIE = "Die";
    public const string FINISH_DEATH = "FinishDeath";
    public const string DROWN = "Drown";

    // Attack Animations
    public const string ATTACK1 = "Attack1";
    public const string ATTACK2 = "Attack2";

    // Spike shooting animations
    public const string AIMING = "Aiming";
    public const string SHOOT = "Shoot";

    // Polish Animations
    public const string FALLING = "Falling";
    public const string DOUBLE_JUMP = "DoubleJump";
    public const string FACE_PLANT = "FinishFaceplant";
    public const string TEETER = "Teeter";  
    public const string SLIDING = "Sliding";  
}
