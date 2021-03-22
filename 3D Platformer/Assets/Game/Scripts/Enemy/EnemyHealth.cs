using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float CurrentHealth = 0f;
    public float StartingHealth = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = StartingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GetComponent<Animator>().SetTrigger(EnemyAnimatorConstants.DIE);
    }
}
