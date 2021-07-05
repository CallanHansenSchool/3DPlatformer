using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatChecker : MonoBehaviour
{
    public bool InCombat = false;

    public static CombatChecker Instance = null;

    public List<EnemyManager> CloseEnemies = new List<EnemyManager>();

    private int timesCheckedInRowWithoutEnemy;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    void UpdateInCombat()
    {
        PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.IN_COMBAT, InCombat);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(GameManager.ENEMY_TAG))
        {
            CloseEnemies.Add(other.gameObject.GetComponent<EnemyManager>()); // Added close enemy to list
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameManager.ENEMY_TAG))
        {
            InCombat = true;
            UpdateInCombat();
            timesCheckedInRowWithoutEnemy = 0;
        } else
        {
            timesCheckedInRowWithoutEnemy++;

            if(timesCheckedInRowWithoutEnemy >= 20)
            {
                InCombat = false;
                UpdateInCombat();
                return;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(GameManager.ENEMY_TAG))
        {
            InCombat = false;
            CloseEnemies.Remove(other.gameObject.GetComponent<EnemyManager>()); // Remove close enemy from list 
            UpdateInCombat();
        }
    }
}
