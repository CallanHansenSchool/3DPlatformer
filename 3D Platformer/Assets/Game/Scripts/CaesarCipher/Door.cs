using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject caesarCipherUI = null;
    [SerializeField] private GameObject openDoorPromptText = null;

    private const KeyCode OPEN_DOOR_KEY = KeyCode.E;

    private bool canOpen = false;

    void Start()
    {
        caesarCipherUI.SetActive(false);
        openDoorPromptText.SetActive(false);
    }

    void Update()
    {
        if(canOpen)
        {
            if (Input.GetKeyDown(OPEN_DOOR_KEY))
            {
                OpenDoor();
            }
        }
    }

    void OpenDoor()
    {
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        caesarCipherUI.GetComponent<CaesarCipher>().UpdateCipher();
        caesarCipherUI.gameObject.SetActive(true);
        PlayerManager.Instance.PlayerMovement.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.CurrentSentence.Length >= GameManager.Instance.WantedWord.Length)
        {         
            if (other.gameObject.CompareTag(GameManager.PLAYER_TAG))
            {
                canOpen = true;
                openDoorPromptText.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(GameManager.PLAYER_TAG))
        {
            canOpen = false;
            openDoorPromptText.SetActive(false);
        }
    }
}