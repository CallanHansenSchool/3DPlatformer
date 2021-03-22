using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour // Update and handle the HUD
{

    #region Sentences
    private const string ALL_LETTERS_COLLECTED = "All the letters have been collected! Approach the door.";
    private const string LETTERS_COLLECTED = "Letters Collected: ";
    #endregion

    public TextMeshProUGUI LettersCollectedText;
    public TextMeshProUGUI AllLettersCollectedText;
    public TextMeshProUGUI LivesRemainingText;

    public Slider HealthBarSlider;

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
        UpdateHUD();
    }

    public void UpdateHUD()
    {
        LettersCollectedText.text = LETTERS_COLLECTED + GameManager.Instance.CurrentSentence;
        LivesRemainingText.text = PlayerPrefs.GetInt(PlayerPrefConstants.PLAYER_LIVES, 3).ToString();

        HealthBarSlider.value = PlayerHealth.Instance.CurrentHealth;

        if (GameManager.Instance.LettersCollected.Count == GameManager.Instance.WantedWord.Length) // All letters are collected
        {
            AllLettersCollectedText.text = ALL_LETTERS_COLLECTED;
        }
    }
}
