using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float CurrentHealth = 0f;
    [SerializeField] private float startingHealth = 4.0f;

    [SerializeField] private Slider healthBar = null;

    private bool dead = false;

    public GameObject HealthBurst = null;
    public GameObject EnemyCanvas = null;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = startingHealth;

        healthBar.maxValue = startingHealth;
        healthBar.value = CurrentHealth;
    }

    void UpdateUI()
    {
        healthBar.value = CurrentHealth;
    }

    public void TakeDamage(float damageToTake, bool _lightAttack)
    {     
        CurrentHealth -= damageToTake;
        UpdateUI();

        if (CurrentHealth <= 0)
        {
            Die();
        } else
        {
            if(_lightAttack)
            {
                GetComponent<EnemyManager>().Anim.SetTrigger(EnemyAnimatorConstants.LIGHT_ATTACK_HIT_REACTION);
            } else
            {
                GetComponent<EnemyManager>().Anim.SetTrigger(EnemyAnimatorConstants.HEAVY_ATTACK_HIT_REACTION);
            }

            AudioManager.Instance.PlayAudio("SnakeTakeDamage");

            GetComponent<EnemyManager>().Agent.enabled = false;
        }
    }

    void Die()
    {
        if(!dead)
        {
            EnemyCanvas.SetActive(false);
            AudioManager.Instance.PlayAudio("SnakeDie");
            dead = true;
            GetComponent<EnemyManager>().Anim.SetTrigger(EnemyAnimatorConstants.DIE);
            transform.tag = "Untagged";
        }       
    }
}
