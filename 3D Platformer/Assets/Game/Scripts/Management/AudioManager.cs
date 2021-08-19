using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] playerSounds;
    [SerializeField] private Sound[] snakeSounds;
    [SerializeField] private Sound[] environmentalSounds;
    [SerializeField] private Sound[] uiSounds;
    [SerializeField] private Sound[] otherSounds;

    public List<Sound> Sounds = new List<Sound>();

    public static AudioManager Instance;

    void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }

        #endregion

        DontDestroyOnLoad(gameObject);
        AddSounds();
  

        foreach (Sound s in Sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;
            s.source.volume = s.Volume;
            s.source.pitch = s.Pitch;
            s.source.loop = s.Loop;
            s.source.playOnAwake = s.PlayOnStart;
        }
    }

    void AddSounds()
    {
        Sounds.AddRange(playerSounds);
        Sounds.AddRange(snakeSounds);
        Sounds.AddRange(environmentalSounds);
        Sounds.AddRange(uiSounds);
        Sounds.AddRange(otherSounds);
    }

    public void AddSound(Sound _sound)
    {    
        _sound.source = gameObject.AddComponent<AudioSource>();
        _sound.source.clip = _sound.Clip;
        _sound.source.volume = _sound.Volume;
        _sound.source.pitch = _sound.Pitch;
        _sound.source.loop = _sound.Loop;
        _sound.source.playOnAwake = _sound.PlayOnStart;

        if(_sound.PlayOnStart)
        {
            _sound.source.Play();
        }

        Instance.Sounds.Add(_sound);
    }

    public void PlayAudio(string _name)
    {
        //Sound s = Array.Find(Sounds, sound => sound.Name == _name); // Sets sound veriable to the sound which is equal to the same name in the Sounds[] array.

        foreach (Sound s in Sounds)
        {
            if (s == null)
            {
                Debug.LogError("Sound: " + _name + " doesn't exist!");
            } else if (s.Name == _name)
            {
                s.source.Play();
                return;
            }
        }           
    }
    public void StopAudio(string _name)
    {
        //Sound s = Array.Find(Sounds, sound => sound.Name == _name); // Sets sound veriable to the sound which is equal to the same name in the Sounds[] array.

        foreach (Sound s in Sounds)
        {
            if (s == null)
            {
                Debug.LogError("Sound: " + _name + " doesn't exist!");
            }
            else if (s.Name == _name)
            {
                s.source.Stop();
                return;
            }
        }
    }
}
