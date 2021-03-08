using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public enum COLLECTABLE_TYPE { COIN, LETTER, }

    public COLLECTABLE_TYPE CollectableType;

    public char Letter = 'A';

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameManager.PLAYER_TAG))
        {
            // Play pickup effect
            switch(CollectableType)
            {
                case COLLECTABLE_TYPE.COIN:
                    // Pickup coin
                    break;

                case COLLECTABLE_TYPE.LETTER:
                    GameManager.Instance.NumOfLettersCollected++;
                    GameManager.Instance.LettersCollected.Add(Letter);
                    break;
            }

            GameManager.Instance.UpdateLettersUI();
            gameObject.SetActive(false);          
        }
    }
}
