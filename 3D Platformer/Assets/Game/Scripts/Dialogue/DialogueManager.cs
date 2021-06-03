using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sentenceText = null;
    [SerializeField] private TextMeshProUGUI nameText = null;
    [SerializeField] private GameObject dialogueBox = null;
    [SerializeField] private GameObject continueButton = null;

    [SerializeField] private int sentenceCount = 0;
    private int curIndex = 0;

    public NPC Sender = null;

    [SerializeField] private Button option1Button = null;
    [SerializeField] private Button option2Button = null;
    [SerializeField] private Button option3Button = null;

    public bool InDialogue = false;

    #region Singleton
    public static DialogueManager Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
            Debug.LogError("There were more than one dialogue manager in the scene!");
        }
    }

    #endregion

    void Start()
    {
        dialogueBox.SetActive(false);        
    }

    void SetOptionTexts()
    {
        /*  
        Debug.Log("Option 1: " + Sender.Option1[curIndex]);
        Debug.Log("Option 2: " + Sender.Option2[curIndex]);
        Debug.Log("Option 3: " + Sender.Option3[curIndex]);
        */     

        option1Button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Sender.Option1[curIndex];
        option2Button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Sender.Option2[curIndex];
        option3Button.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = Sender.Option3[curIndex];      
    }

    public void StartDialogue(string _startSentence)
    {
        InDialogue = true;

        sentenceCount = Sender.Option1Sentences.Length - 1;

        GameManager.Instance.MainVirtualCamera.enabled = false;
        GameManager.Instance.aimingCamera.enabled = false;
        curIndex = 0;
        dialogueBox.SetActive(true);

        
        PlayerManager.Instance.PlayerMovement.enabled = false;

        EnableDisableOptionButtons(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        nameText.text = Sender.Name;
     
        sentenceText.text = "";

        sentenceText.text += _startSentence;

        if (Sender.Option1.Length == 0)
        {
            continueButton.SetActive(true);
            option1Button.gameObject.SetActive(false);
            option2Button.gameObject.SetActive(false);
            option3Button.gameObject.SetActive(false);
        } else
        {
            continueButton.SetActive(false);
            option1Button.gameObject.SetActive(true);
            option2Button.gameObject.SetActive(true);
            option3Button.gameObject.SetActive(true);
            StartCoroutine(EnsureOptionChange());
        }
    }

    IEnumerator EnsureOptionChange()
    {
        SetOptionTexts();
        yield return new WaitForSeconds(0.05f);
        SetOptionTexts();
    }

    public void EndDialogue()
    {
        Debug.Log("Dialogue has ended!");
        Sender.CanTalk = false;
        PlayerManager.Instance.PlayerMovement.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameManager.Instance.MainVirtualCamera.enabled = true;
        GameManager.Instance.aimingCamera.enabled = true;
        InDialogue = false;
        dialogueBox.SetActive(false);
    }

    void EnableDisableOptionButtons(bool _enabled)
    {
        option1Button.gameObject.SetActive(_enabled);
        option2Button.gameObject.SetActive(_enabled);
        option3Button.gameObject.SetActive(_enabled);
    }

    void ShowContinueButton()
    {
        Debug.Log("Showing continue button");
        continueButton.SetActive(true);
        EnableDisableOptionButtons(false);
    }

    public void TypeSentence(string _sentence = "")
    { 
        Debug.Log("Typed a sentence");

        if (_sentence != null)
        {
            sentenceText.text = "";
            sentenceText.text += _sentence;
        }

        StartCoroutine(EnsureOptionChange());
        curIndex++;
      
        if (curIndex > sentenceCount)
        {
            ShowContinueButton();
            return;
        }
    }

    public void Option1()
    {
        TypeSentence(Sender.Option1Sentences[curIndex]);
    }

    public void Option2()
    {
        TypeSentence(Sender.Option2Sentences[curIndex]);
    }

    public void Option3()
    { 
        TypeSentence(Sender.Option3Sentences[curIndex]);
    }
}
