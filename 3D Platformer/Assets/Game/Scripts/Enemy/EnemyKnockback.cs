using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    public int hitNumber = 0;

    [SerializeField] private float knockbackSpeed = 4.5f; // How fast the enemy gets knocked back
    [SerializeField] private float knockbackTime = 1f; // How long the enemy gets knocked back for

    [SerializeField] private float knockbackHeight = 1f; // How long the enemy gets knocked back for

    public bool KnockingBack = false;

    private Vector3 direction = Vector3.forward;

    void Update()
    {
        GetComponent<EnemyManager>().Anim.SetBool(EnemyAnimatorConstants.KNOCKBACK, KnockingBack);

        if (hitNumber >= 3)
        {
            if (!KnockingBack)
            {
                StartCoroutine(TimeKnockback());         
                hitNumber = 0;
            }
        }

        if (KnockingBack)
        {
            GetComponent<EnemyManager>().Agent.enabled = false;
            GetComponent<EnemyManager>().Agent.destination = transform.position;

            direction = transform.position - PlayerManager.Instance.transform.position;                    
           
            direction.y = 0;

            //Vector3.Lerp(transform.position, direction.normalized, knockbackSpeed * Time.deltaTime);
        }
    }

    IEnumerator TimeKnockback()
    {
        KnockingBack = true;
        yield return new WaitForSeconds(knockbackTime);
        KnockingBack = false;
    }
}
