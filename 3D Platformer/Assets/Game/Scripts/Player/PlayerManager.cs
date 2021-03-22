using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField] private float deathTime = 4.0f;
    [SerializeField] private int defaultLives = 3;

    public int livesRemaining = 3;

    public bool Dead = false;

    [SerializeField] private GameObject gameOverCanvas = null;

    private Vector3 startPos;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        startPos = transform.position;
        gameOverCanvas.SetActive(false);
        livesRemaining = PlayerPrefs.GetInt(PlayerPrefConstants.PLAYER_LIVES, 3);
    }

    public IEnumerator BeginDeath()
    {
        if (!Dead)
        {
            // Play Death animation

            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            gameObject.GetComponent<PlayerMovement>().enabled = false;

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

    void GameOver()
    {
        gameOverCanvas.SetActive(true);
        GetComponent<PlayerMovement>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerPrefs.SetInt(PlayerPrefConstants.PLAYER_LIVES, defaultLives);
    }

    void Respawn()
    {
        // Debug.Log("Respawned player to position" + startPos);
        PlayerHealth.Instance.ResetHealth();
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = true;
        gameObject.GetComponent<PlayerMovement>().enabled = true;
        Dead = false;
        Instance.transform.position = startPos;
        Physics.SyncTransforms(); // Update the changed position
    }
}
