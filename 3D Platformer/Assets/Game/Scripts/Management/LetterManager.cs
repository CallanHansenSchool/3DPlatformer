using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterManager : MonoBehaviour
{
    public List<GameObject> letterCollectableLocations = new List<GameObject>();
    public string WantedWord = "Example";

    [SerializeField] private List<Collectable> charactersInWantedWord = new List<Collectable>();

    public string EncryptedSentence = "";
    public string CurrentSentence = ""; // The current word spelt which needs to be sorted into the correct order, so that the sentence doesnt get jumbled up if the letters are collected in the wrong order
    public List<char> LettersCollected = new List<char>(); // The list of the letters which the player has collected.

    public int EncryptionAmount = 2; // Change depending on game difficulty
    public int NumOfLettersCollected = 0;

    public static LetterManager Instance;

    [SerializeField] private Collectable[] letterCollectables;

    private int currentCharacterIndex = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CheckLetterCount();
    }

    public void CheckLetterCount() // Ensuring that there are not too many letters in the level
    {
        if (letterCollectableLocations.Count > WantedWord.Length) // Check if there are extra spots for letters to spawn which are not needed
        {
            Destroy(letterCollectableLocations[letterCollectableLocations.Count - 1]);
            letterCollectableLocations.RemoveAt(letterCollectableLocations.Count - 1);
            CheckLetterCount();
        }
        else
        {
            SpawnCollectables();
        }
    }

    public void UpdateLettersUI()
    {
        CurrentSentence += LettersCollected[LettersCollected.Count - 1];
    }

    public void SpawnCollectables()
    {
        for (int i = 0; i < charactersInWantedWord.Count; i++)
        {
            switch (charactersInWantedWord[i].Letter)
            {
                #region UpperCase
                case 'A':                 
                    InstantiateCollectable(0);
                    break;

                case 'B':
                    InstantiateCollectable(1);
                    break;

                case 'C':
                    InstantiateCollectable(2);
                    break;

                case 'D':
                    InstantiateCollectable(3);
                    break;

                case 'E':
                    InstantiateCollectable(4);
                    break;

                case 'F':
                    InstantiateCollectable(5);
                    break;

                case 'G':
                    InstantiateCollectable(6);
                    break;

                case 'H':
                    InstantiateCollectable(7);
                    break;

                case 'I':
                    InstantiateCollectable(8);
                    break;

                case 'J':
                    InstantiateCollectable(9);
                    break;

                case 'K':
                    InstantiateCollectable(10);
                    break;

                case 'L':
                    InstantiateCollectable(11);
                    break;

                case 'M':
                    InstantiateCollectable(12);
                    break;

                case 'N':
                    InstantiateCollectable(13);
                    break;

                case 'O':
                    InstantiateCollectable(14);
                    break;

                case 'P':
                    InstantiateCollectable(15);
                    break;

                case 'Q':
                    InstantiateCollectable(16);
                    break;

                case 'R':
                    InstantiateCollectable(17);
                    break;

                case 'S':
                    InstantiateCollectable(18);
                    break;

                case 'T':
                    InstantiateCollectable(19);
                    break;

                case 'U':
                    InstantiateCollectable(20);
                    break;

                case 'V':
                    InstantiateCollectable(21);
                    break;

                case 'W':
                    InstantiateCollectable(22);
                    break;

                case 'X':
                    InstantiateCollectable(23);
                    break;

                case 'Y':
                    InstantiateCollectable(24);
                    break;

                case 'Z':
                    InstantiateCollectable(25);
                    break;

                #endregion

                #region LowerCase
                case 'a':
                    InstantiateCollectable(0);
                    break;

                case 'b':
                    InstantiateCollectable(1);
                    break;

                case 'c':
                    InstantiateCollectable(2);
                    break;

                case 'd':
                    InstantiateCollectable(3);
                    break;

                case 'e':
                    InstantiateCollectable(4);
                    break;

                case 'f':
                    InstantiateCollectable(5);
                    break;

                case 'g':
                    InstantiateCollectable(6);
                    break;

                case 'h':
                    InstantiateCollectable(7);
                    break;

                case 'i':
                    InstantiateCollectable(8);
                    break;

                case 'j':
                    InstantiateCollectable(9);
                    break;

                case 'k':
                    InstantiateCollectable(10);
                    break;

                case 'l':
                    InstantiateCollectable(11);
                    break;

                case 'm':
                    InstantiateCollectable(12);
                    break;

                case 'n':
                    InstantiateCollectable(13);
                    break;

                case 'o':
                    InstantiateCollectable(14);
                    break;

                case 'p':
                    InstantiateCollectable(15);
                    break;

                case 'q':
                    InstantiateCollectable(16);
                    break;

                case 'r':
                    InstantiateCollectable(17);
                    break;

                case 's':
                    InstantiateCollectable(18);
                    break;

                case 't':
                    InstantiateCollectable(19);
                    break;

                case 'u':
                    InstantiateCollectable(20);
                    break;

                case 'v':
                    InstantiateCollectable(21);
                    break;

                case 'w':
                    InstantiateCollectable(22);
                    break;

                case 'x':
                    InstantiateCollectable(23);
                    break;

                case 'y':
                    InstantiateCollectable(24);
                    break;

                case 'z':
                    InstantiateCollectable(25);
                    break;
                    #endregion
            }
        }      
    }

    private void InstantiateCollectable(int _index)
    {
        GameObject collectable = Instantiate(letterCollectables[_index].gameObject, letterCollectableLocations[currentCharacterIndex].transform.position, Quaternion.identity);
        collectable.GetComponent<Collectable>().Letter = WantedWord[currentCharacterIndex];

        currentCharacterIndex++;

        Debug.Log("Spawned the letter " + letterCollectables[_index].name);

        HUD.Instance.UpdateLetterCount();
    }
}
