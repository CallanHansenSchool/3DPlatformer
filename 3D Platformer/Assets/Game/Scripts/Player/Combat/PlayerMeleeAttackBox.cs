using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackBox : MonoBehaviour
{
    public float attackDamage = 2f;

    [SerializeField] private bool lightAttack = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(attackDamage, lightAttack);
            Debug.Log("Enemy took melee damage");
        }
    }
}
