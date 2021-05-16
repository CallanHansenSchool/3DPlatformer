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

    public PlayerMovement PlayerMovement;
    public PlayerSlopeSlide PlayerSlopeSlide;

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
        startPos = transform.position;
        gameOverCanvas.SetActive(false);
        livesRemaining = PlayerPrefs.GetInt(PlayerPrefConstants.PLAYER_LIVES, 3);
    }

    public IEnumerator BeginDeath(string deathType = "") // Handles the death of the player
    {
        if (!Dead)
        {
            if(deathType == "")
            {
                Anim.SetTrigger(PlayerAnimationConstants.DIE);
            } else if (deathType == "Enemy")
            {
                Anim.SetTrigger(PlayerAnimationConstants.DIE);
            } else if(deathType == "Drown")
            {
                Anim.SetTrigger(PlayerAnimationConstants.DROWN);
            }         

            PlayerMovement.enabled = false;

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
        Dead = false;
        Anim.SetTrigger(PlayerAnimationConstants.FINISH_DEATH);
        Instance.transform.position = startPos;
        Physics.SyncTransforms(); // Update the changed position
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameManager.WATER_TAG))
        {          
            StartCoroutine(BeginDeath("Drown"));
        }
    }
}
