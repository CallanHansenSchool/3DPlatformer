using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private Sound lightAttack1;
    [SerializeField] private Sound lightAttack2;
    [SerializeField] private Sound lightAttack3;

    [SerializeField] private Sound faceplant;

    [SerializeField] private Sound block;

    [SerializeField] private Sound heavyAttackBuildup;
    [SerializeField] private Sound heavyAttack;

    [SerializeField] private Sound bodySlamFall;
    [SerializeField] private Sound bodySlamLand;

    [SerializeField] private Sound jump;


    void Start()
    {

        #region Adding all sounds to AudioManager
        AudioManager.Instance.AddSound(lightAttack1);
        AudioManager.Instance.AddSound(lightAttack2);
        AudioManager.Instance.AddSound(lightAttack3);

        AudioManager.Instance.AddSound(faceplant);

        AudioManager.Instance.AddSound(block);
        AudioManager.Instance.AddSound(heavyAttackBuildup);
        AudioManager.Instance.AddSound(heavyAttack);
        AudioManager.Instance.AddSound(bodySlamLand);
        AudioManager.Instance.AddSound(bodySlamFall);
        #endregion
    }


    #region Called From Animation Events
    public void PlayJumpSound()
    {
        Debug.Log("Played Jump Sound");
        AudioManager.Instance.PlayAudio(jump.Name);
    }
    
    public void PlayLightAttack1()
    {
        AudioManager.Instance.PlayAudio(lightAttack1.Name);
    }

    public void PlayLightAttack2()
    {
        AudioManager.Instance.PlayAudio(lightAttack2.Name);
    }

    public void PlayLightAttack3()
    {
        AudioManager.Instance.PlayAudio(lightAttack3.Name);
    }

    public void PlayFaceplantSound()
    {
        Debug.Log("Played faceplant sound");
        AudioManager.Instance.PlayAudio(faceplant.Name);
    }

    public void PlayBlockSound()
    {
        AudioManager.Instance.PlayAudio(block.Name);
    }

    public void PlayHeavyAttackBuildupSound()
    {
        AudioManager.Instance.PlayAudio(heavyAttackBuildup.Name);
    }

    public void PlayBodySlamLandSound()
    {
        Debug.Log("Played body slam sound");
        AudioManager.Instance.PlayAudio(bodySlamLand.Name);
    }

    public void PlayBodySlamFallSound()
    {
        AudioManager.Instance.PlayAudio(bodySlamFall.Name);
    }

    #endregion
}
