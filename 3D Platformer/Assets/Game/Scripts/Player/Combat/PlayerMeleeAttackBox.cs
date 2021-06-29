using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackBox : MonoBehaviour
{
    public float attackDamage = 2f;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            Debug.Log("Enemy took melee damage");
        }
    }
}
