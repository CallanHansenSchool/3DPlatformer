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
    private EnemyKnockback enemyknockback = null;

    // Start is called before the first frame update
    void Start()
    {
        enemyknockback = GetComponent<EnemyKnockback>();

        CurrentHealth = startingHealth;

        healthBar.maxValue = startingHealth;
        healthBar.value = CurrentHealth;
    }

    void UpdateUI()
    {
        healthBar.value = CurrentHealth;
    }

    public void TakeDamage(float damageToTake)
    {
        CurrentHealth -= damageToTake;
        UpdateUI();

        enemyknockback.hitNumber++;

        if (CurrentHealth <= 0)
        {
            Die();
        } else
        {
            GetComponent<EnemyManager>().Anim.SetTrigger(EnemyAnimatorConstants.TAKE_DAMAGE);
            GetComponent<EnemyManager>().Agent.enabled = false;
        }
    }

    void Die()
    {
        if(!dead)
        {
            dead = true;
            GetComponent<EnemyManager>().Anim.SetTrigger(EnemyAnimatorConstants.DIE);
            transform.tag = "Untagged";
        }       
    }
}
