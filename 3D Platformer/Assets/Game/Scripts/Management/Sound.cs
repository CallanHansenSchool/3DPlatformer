using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip Clip = null;

    public string Name = "";

    [Range(0f, 1f)]
    public float Volume = 1;

    [Range(0.1f, 3f)]
    public float Pitch = 1;

    [Range(0.1f, 1f)]
    public float Speed = 1;

    public bool Loop = false;
    public bool PlayOnStart = false;

    [HideInInspector]
    public AudioSource source;
}
