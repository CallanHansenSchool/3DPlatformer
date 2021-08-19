using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepAudio : MonoBehaviour
{
    public List<AudioClip> footstepSounds = new List<AudioClip>();
    public AudioClip JumpSound;
    public AudioClip LandSound;

    private FootstepSwapper footstepSwapper;

    private int currentFootstepIndex;

    private AudioSource audioSource = null;

    FootstepCollection collection;

    void Start()
    {
        footstepSwapper = GetComponent<FootstepSwapper>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayLandSound()
    {
        footstepSwapper.CheckLayers();
        audioSource.volume = collection.LandVolume;
        audioSource.PlayOneShot(collection.landSound);
        Debug.Log("Played land sound!");
    }

    public void PlayJumpSound()
    {
        footstepSwapper.CheckLayers();
        audioSource.volume = collection.JumpVolume;
        audioSource.PlayOneShot(collection.jumpSound);
        Debug.Log("Played jump sound!");
    }

    public void PlayFootstepAudio()
    {
        Debug.Log("Played footstep audio");

        footstepSwapper.CheckLayers();
        audioSource.volume = collection.FootstepVolume;

        AudioClip clip = footstepSounds[currentFootstepIndex];
        
        audioSource.PlayOneShot(clip);
      
        currentFootstepIndex = (currentFootstepIndex + 1) % footstepSounds.Count;
    }

    public void SwapFootsteps(FootstepCollection _collection)
    {
        Debug.Log("Swapped footsteps!");

        footstepSounds.Clear();     

        for (int i = 0; i < _collection.footstepSounds.Count; i++)
        {
            footstepSounds.Add(_collection.footstepSounds[i]);
        }

        collection = _collection;

        audioSource.volume = _collection.FootstepVolume;
        JumpSound = _collection.jumpSound;
        LandSound = _collection.landSound;
    }
}
