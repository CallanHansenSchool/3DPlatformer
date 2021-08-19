using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance = null;

    [SerializeField] private float deathTime = 4.0f;
    [SerializeField] private int defaultLives = 3;

    public int livesRemaining = 3;

    public bool Dead = false;

    [SerializeField] private GameObject gameOverCanvas = null;

    private Vector3 startPos = Vector3.zero;

    public Animator Anim = null;

    public PlayerMovement PlayerMovement = null;
    public PlayerSlopeSlide PlayerSlopeSlide = null;
    public PlayerLadderClimbing PlayerLadderClimb = null;
    public PlayerMelee PlayerMelee = null;

    public bool Stunned = false;

    void Awake()
    {
        Anim = GetComponentInChildren<Animator>();

        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    void Start()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerSlopeSlide = GetComponent<PlayerSlopeSlide>();
        PlayerLadderClimb = GetComponent<PlayerLadderClimbing>();
        PlayerMelee = GetComponent<PlayerMelee>();
        startPos = transform.position;
        gameOverCanvas.SetActive(false);
        livesRemaining = PlayerPrefs.GetInt(PlayerPrefConstants.PLAYER_LIVES, 3);
    }

    public IEnumerator BeginDeath(string deathType = "") // Handles the death of the player
    {
        if (!Dead)
        {
            switch(deathType)
            {
                case "":
                    Anim.SetTrigger(PlayerAnimationConstants.DIE);
                    break;

                case "Enemy":
                    Anim.SetTrigger(PlayerAnimationConstants.DIE);
                    break;

                case "Drown":
                    Anim.SetTrigger(PlayerAnimationConstants.DROWN);
                    break;

                case "Fallpit":
                    Anim.SetTrigger(PlayerAnimationConstants.DIE);
                    break;
            }     

            // Disable all player components to stop player input
            PlayerMovement.enabled = false;
            PlayerMelee.enabled = false;
            PlayerSlopeSlide.enabled = false;
            PlayerLadderClimb.enabled = false;

            livesRemaining -= 1; // Taking away a life
            PlayerPrefs.SetInt(PlayerPrefConstants.PLAYER_LIVES, livesRemaining);  // Saving the value

            HUD.Instance.UpdateHUD();

            Dead = true;

            yield return new WaitForSeconds(deathTime);

            if (livesRemaining > 0)
            {
                Respawn();
            }
            else
            {
                GameOver();
            }
        }
    }

    void GameOver() // When the player runs out of lives
    {
        gameOverCanvas.SetActive(true);
        PlayerMovement.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerPrefs.SetInt(PlayerPrefConstants.PLAYER_LIVES, defaultLives);
    }

    void Respawn()
    {
        // Debug.Log("Respawned player to position " + startPos);
        PlayerHealth.Instance.ResetHealth();
        PlayerMovement.enabled = true;
        PlayerMelee.enabled = true;
        PlayerLadderClimb.enabled = true;
        PlayerSlopeSlide.enabled = true;
        Dead = false;
        Anim.SetTrigger(PlayerAnimationConstants.FINISH_DEATH);
        Instance.transform.position = startPos;
        Physics.SyncTransforms(); // Update the changed position
    }

    void OnTriggerEnter(Collider other)  // Called when the player touches something
    {
        if (other.gameObject.CompareTag(GameManager.WATER_TAG))
        {          
            StartCoroutine(BeginDeath("Drown"));
        } 
        
        if(other.gameObject.CompareTag(GameManager.FALL_PIT))
        {
            StartCoroutine(BeginDeath("FallPit"));
        }
    }
}
