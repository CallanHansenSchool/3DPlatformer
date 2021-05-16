using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class NPC : MonoBehaviour
{
    public string Name = "";
    
    [TextArea(3, 8)]
    [SerializeField] private string firstSentence = "";

    [TextArea(3, 8)]
    public string[] Option1 = new string[3];
    [TextArea(3, 8)]
    public string[] Option2 = new string[3];
    [TextArea(3, 8)]
    public string[] Option3 = new string[3];

    [TextArea(3, 8)]
    public string[] Option1Sentences = new string[3];
    [TextArea(3, 8)]   
    public string[] Option2Sentences = new string[3];
    [TextArea(3, 8)]
    public string[] Option3Sentences = new string[3];

    public AudioClip[] option1SentenceAudio;
    public AudioClip[] option2SentenceAudio;
    public AudioClip[] option3SentenceAudio;

    [SerializeField] private GameObject talkText = null;

    public bool CanTalk = false;

    private const KeyCode TALK_KEY = KeyCode.E;

    [SerializeField] private bool canRepeatDialogue = true;
    private bool repeatedDialogue = false;

    void Start()
    {
        talkText.SetActive(false);
    }

    void Update()
    {
        if(CanTalk)
        {
            if (Input.GetKeyDown(TALK_KEY))
            {
                if(!repeatedDialogue)
                {
                    DialogueManager.Instance.Sender = this;
                    DialogueManager.Instance.StartDialogue(firstSentence);

                    if(canRepeatDialogue)
                    {
                        repeatedDialogue = false;
                    } else
                    {
                        repeatedDialogue = true;
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameManager.PLAYER_TAG))
        {
            talkText.SetActive(true);
            CanTalk = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag(GameManager.PLAYER_TAG))
        {
            talkText.SetActive(false);
            CanTalk = false;
        }
    }
}
