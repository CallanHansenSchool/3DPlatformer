using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour // Update and handle the HUD
{

    #region Sentences
    private const string ALL_LETTERS_COLLECTED = "All the letters have been collected! Approach the door.";
    private const string LETTERS_COLLECTED = "Letters Collected: ";
    #endregion

    public TextMeshProUGUI LettersCollectedText;
    public TextMeshProUGUI AllLettersCollectedText;

    public static HUD Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        AllLettersCollectedText.text = "";
    }


    public void UpdateHUD()
    {
        LettersCollectedText.text = LETTERS_COLLECTED + GameManager.Instance.CurrentSentence;

        if (GameManager.Instance.LettersCollected.Count == GameManager.Instance.WantedWord.Length) // All letters are collected
        {
            AllLettersCollectedText.text = ALL_LETTERS_COLLECTED;
        }
    }
}
