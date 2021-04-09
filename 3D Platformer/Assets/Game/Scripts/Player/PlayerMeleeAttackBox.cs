using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackBox : MonoBehaviour
{
    private PlayerMelee melee;

    void Start()
    {
        melee = GetComponentInParent<PlayerMelee>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(melee.AttackDamage);
            Debug.Log("Enemy took melee damage");
        }
    }
}
