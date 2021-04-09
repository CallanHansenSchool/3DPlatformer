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

    public void TakeDamage(float damageToTake)
    {
        CurrentHealth -= damageToTake;
        UpdateUI();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if(!dead)
        {
            dead = true;
            GetComponentInChildren<Animator>().SetTrigger(EnemyAnimatorConstants.DIE);
        }       
    }
}
