using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Camera MainCamera;

    public static GameManager Instance;

    #region Tags
    public const string PLAYER_TAG = "Player";
    #endregion

    public string WantedWord = "Example"; // Inputted word at the start of game
    public string EncryptedSentence = "";
    public string CurrentSentence = ""; // The current word spelt which needs to be sorted into the correct order, so that the sentence doesnt get jumbled up if the letters are collected in the wrong order
    public List<char> LettersCollected = new List<char>(); // The list of the letters which the player has collected.

    public int EncryptionAmount = 2; // Change depending on game difficulty
    public int NumOfLettersCollected = 0;
    
    public List<GameObject> letterCollectableLocations = new List<GameObject>();

    [SerializeField] private GameObject letterCollectablePrefab;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There are 2 instances of the GameManager!");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //SpawnCollectables();
        CheckLetterCount();
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
        HUD.Instance.UpdateHUD();
    }
}