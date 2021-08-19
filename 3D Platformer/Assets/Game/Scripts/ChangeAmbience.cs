using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAmbience : MonoBehaviour
{
    private AmbienceManager ambienceManager;

    [SerializeField] private AudioSource sourceToChange;

    void Start()
    {
        ambienceManager = GetComponentInParent<AmbienceManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(GameManager.PLAYER_TAG))
        {
            ambienceManager.ChangeAmbience(sourceToChange);
        }      
    }
}
