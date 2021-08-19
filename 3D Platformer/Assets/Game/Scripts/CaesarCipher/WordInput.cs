using UnityEngine;
using TMPro;
using System;

public class WordInput : MonoBehaviour
{
    [SerializeField] private TMP_InputField wordInputField;
    [SerializeField] private TextMeshProUGUI wordStatusText;

    private const string CORRECT_TEXT = "Word was correct!";
    private const string WRONG_TEXT = "Word was wrong!";

    void Start()
    { 
        wordInputField.onValueChanged.AddListener(delegate { RemoveSpaces(); }); // Adding listener to input field to block spaces from being entered

        wordStatusText.text = "";
    }

    void RemoveSpaces() // Disable the use of spaces in the word input field
    {
        wordInputField.text = wordInputField.text.Replace(" ", ""); // Replace spaces with nothing
    }

    public void CheckWord()
    {
        if (string.Equals(wordInputField.text, LetterManager.Instance.WantedWord, StringComparison.OrdinalIgnoreCase)) // Checking if the players input is the same as the correct word, ignoring caps
        {
            wordStatusText.text = CORRECT_TEXT;
            SceneManagement.Instance.LoadScene(GameManager.CREDITS);
        } else
        {
            wordStatusText.text = WRONG_TEXT;
        }
    }
}
