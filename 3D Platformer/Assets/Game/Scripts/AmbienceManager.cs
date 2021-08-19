using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceManager : MonoBehaviour
{
    public AudioSource CurrentAmbience;

    void Start()
    {
        CurrentAmbience.Play();
    }

    public void ChangeAmbience(AudioSource _ambienceToChangeTo)
    {
        AudioSource oldSource = _ambienceToChangeTo;
        oldSource.Stop();

        CurrentAmbience = _ambienceToChangeTo;
        CurrentAmbience.Play();
    }
}
