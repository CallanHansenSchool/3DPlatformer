using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject CaesarCipherUI;

    void Start()
    {
        CaesarCipherUI.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.CurrentSentence.Length >= GameManager.Instance.WantedWord.Length)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (other.gameObject.CompareTag(GameManager.PLAYER_TAG))
            {
                CaesarCipherUI.GetComponent<CaesarCipher>().UpdateCipher();
                CaesarCipherUI.gameObject.SetActive(true);
            }
        }
    }
}