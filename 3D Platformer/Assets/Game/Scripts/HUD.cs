using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour // Update and handle the HUD
{
    #region Sentences
    private const string ALL_LETTERS_COLLECTED = "All the letters have been collected! Approach the door.";
    #endregion

    public Slider HealthBarSlider;

    #region TextMesh
    [Header("Texts")]
    public TextMeshProUGUI LettersCollectedText = null;
    public TextMeshProUGUI AllLettersCollectedText = null;
    public TextMeshProUGUI LivesRemainingText = null;
    public TextMeshProUGUI CommonCollectableText = null;
    public TextMeshProUGUI RareCollectableText = null;
    public TextMeshProUGUI LettersRemainingText = null;
    #endregion 

    public static HUD Instance;

    private int amountOfLetters;

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

        UpdateLetterCount();
    }

    public void UpdateLetterCount()
    {
        amountOfLetters = 0;

        Collectable[] _letterCollectablesTemp = FindObjectsOfType<Collectable>();

        for (int i = 0; i < _letterCollectablesTemp.Length; i++)
        {
            if (_letterCollectablesTemp[i].CollectableType == Collectable.COLLECTABLE_TYPE.LETTER) 
            {
                amountOfLetters++;
            }
        }

        LettersRemainingText.text = amountOfLetters.ToString();
    }

    public void UpdateHUD()
    {
        CommonCollectableText.text = CollectableManager.Instance.CommonCollectablesCollected.ToString() + " / " + CollectableManager.COMMON_COLLECTABLES_NEEDED_FOR_LIFE.ToString();
        RareCollectableText.text = CollectableManager.Instance.RareCollectablesCollected.ToString() + " / " + (GameManager.Instance.RareCollectables.Length).ToString(); 
      
        LettersCollectedText.text = LetterManager.Instance.CurrentSentence;
        LivesRemainingText.text = PlayerPrefs.GetInt(PlayerPrefConstants.PLAYER_LIVES, 3).ToString();

        HealthBarSlider.value = PlayerHealth.Instance.CurrentHealth;

        if (LetterManager.Instance.LettersCollected.Count == LetterManager.Instance.WantedWord.Length) // All letters are collected
        {
            AllLettersCollectedText.text = ALL_LETTERS_COLLECTED;
        }
    }
}
