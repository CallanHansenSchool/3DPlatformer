using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Collectable : MonoBehaviour
{
    public enum COLLECTABLE_TYPE { COMMON, RARE, LIFE, LETTER }

    public COLLECTABLE_TYPE CollectableType;

    public char Letter = 'A';

    [SerializeField] private float moveTowardPlayerDistance = 0.3f;
    [SerializeField] private float moveTowardPlayerSpeed = 1.2f;

    void Update()
    {
        if(Vector3.Distance(PlayerManager.Instance.gameObject.transform.position, transform.position) < moveTowardPlayerDistance)
        {
            transform.position = Vector3.Slerp(transform.position, PlayerManager.Instance.gameObject.transform.position, moveTowardPlayerSpeed * Time.deltaTime); // Move towards the player when they are near
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameManager.PLAYER_TAG))
        {
            switch (CollectableType)
            {
                case COLLECTABLE_TYPE.COMMON:
                    // Play common collectable pickup effect
                    // Play common collectable sound
                    CollectableManager.Instance.CommonCollectablesCollected++;
                    break;

                case COLLECTABLE_TYPE.RARE:
                    // Play rare collectable pickup effect
                    // Play rare collectable sound
                    CollectableManager.Instance.RareCollectablesCollected++;
                    break;

                case COLLECTABLE_TYPE.LIFE:

                    break;

                case COLLECTABLE_TYPE.LETTER:
                    //Play letter collectable pickup effect
                    // Play letter collectable sound
                    gameObject.SetActive(false);
                    HUD.Instance.UpdateLetterCount();
                    GameManager.Instance.NumOfLettersCollected++;
                    GameManager.Instance.LettersCollected.Add(Letter);
                    GameManager.Instance.UpdateLettersUI();
                    break;
            }

            CollectableManager.Instance.CheckCollectables();
            HUD.Instance.UpdateHUD();
            gameObject.SetActive(false);          
        }
    }
}