using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Footstep Collection", menuName = "Create New Footstep Collection")]
public class FootstepCollection : ScriptableObject
{
    public List<AudioClip> footstepSounds = new List<AudioClip>();   
    public AudioClip jumpSound;
    public AudioClip landSound;

    [Range (0, 1)]
    public float FootstepVolume = 0.1f;
    [Range(0, 1)]
    public float LandVolume = 0.2f;
    [Range(0, 1)]
    public float JumpVolume = 0.5f;
}
