using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private const KeyCode AttackKey = KeyCode.Mouse0;

    private float attackSpeed = 1.5f;

    [SerializeField] private float startAttackSpeed = 0.1f;
    [SerializeField] private GameObject meleeAttackBox = null;
    public float AttackDamage = 2f;

    private bool attacking = false;

    void Update()
    {      
        if (Input.GetKeyDown(AttackKey))
        {
            if (!PauseMenu.Instance.Paused)
            {
                if (!DialogueManager.Instance.InDialogue)
                {
                    if (!PlayerWeapon.Instance.Aiming)
                    {
                        if (!attacking)
                        {
                            PlayerManager.Instance.Anim.SetTrigger(PlayerAnimationConstants.ATTACK1);
                            attackSpeed = startAttackSpeed;
                        }
                    }
                }
            }              
        }

        if (attackSpeed > 0)
        {
            meleeAttackBox.SetActive(true);
            attacking = true;
            attackSpeed -= Time.deltaTime;
        } else
        {
            meleeAttackBox.SetActive(false);
            attacking = false;
        }
    }
}
