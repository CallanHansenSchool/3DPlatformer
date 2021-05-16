using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public Camera MainCamera = null;

    [Header("Virtual Cameras")]
    public CinemachineFreeLook MainVirtualCamera = null;
    public CinemachineFreeLook aimingCamera = null;   

    public static GameManager Instance;

    #region Tags
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";
    public const string CLIMBABLE_TAG = "Climbable";
    public const string WATER_TAG = "Water";
    #endregion

    #region SceneNames
    public const string MAIN_MENU = "MainMenu";
    #endregion

    public string WantedWord = "Example"; // Inputted word at the start of game
    public string EncryptedSentence = "";
    public string CurrentSentence = ""; // The current word spelt which needs to be sorted into the correct order, so that the sentence doesnt get jumbled up if the letters are collected in the wrong order
    public List<char> LettersCollected = new List<char>(); // The list of the letters which the player has collected.

    public int EncryptionAmount = 2; // Change depending on game difficulty
    public int NumOfLettersCollected = 0;

    public GameObject[] RareCollectables = null;
    public List<GameObject> letterCollectableLocations = new List<GameObject>();

    [SerializeField] private GameObject letterCollectablePrefab = null;

    public bool UsingController = false; 

    void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {      
            Destroy(gameObject);
        }
        #endregion

        //DebugResetPlayerPrefs();
    }

    void Start()
    {
        //SpawnCollectables();
        CheckLetterCount();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    void DebugResetPlayerPrefs() // For resetting all playerprefs
    {
        PlayerPrefs.DeleteAll();
    }

    public void CheckLetterCount() // Ensuring that there are not too many letters in the level
    {
        if (letterCollectableLocations.Count > GameManager.Instance.WantedWord.Length) // Check if there are extra spots for letters to spawn which are not needed
        {
            Destroy(letterCollectableLocations[letterCollectableLocations.Count - 1]);
            letterCollectableLocations.RemoveAt(letterCollectableLocations.Count - 1);
            CheckLetterCount(); 
        } else
        {
            SpawnCollectables();
        }
    }

    public void SpawnCollectables()
    {    
        for (int i = 0; i < letterCollectableLocations.Count; i++)
        {
            GameObject collectable = Instantiate(letterCollectablePrefab, letterCollectableLocations[i].transform.position, Quaternion.identity);
          
            collectable.GetComponent<Collectable>().Letter = WantedWord[i];  
        }
    }

    public void UpdateLettersUI()
    {
        CurrentSentence += LettersCollected[LettersCollected.Count - 1];
    }
}