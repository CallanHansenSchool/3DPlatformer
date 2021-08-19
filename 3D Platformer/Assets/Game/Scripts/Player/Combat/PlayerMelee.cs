using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
    private const KeyCode ATTACK_KEY = KeyCode.Mouse0;
    private const KeyCode BLOCK_KEY = KeyCode.Mouse1;
    private const KeyCode BODY_SLAM_KEY = KeyCode.E;

    private float attackSpeed = 1.5f;

    [SerializeField] private float startAttackSpeed = 0.1f;
    [SerializeField] private GameObject lightMeleeAttackBox = null;
    [SerializeField] private GameObject heavyMeleeAttackBox = null;
    [SerializeField] private GameObject bodySlamMeleeAttackBox = null;
  
    private bool attacking = false;

    private const int NUM_OF_ATTACK_ANIMATIONS = 3;
    private int attackIndex = 0; 

    private float timeSinceLastHit = 0;

    private const float TIME_TO_ATTACK_UNTIL_RESET = 0.85f;

    private const float TIME_UNTIL_ATTACK_BUILDUP = 0.35f;
    private const float TIME_UNTIL_HEAVY_ATTACK = 1.35f;
    private const float ACCEPTED_HEAVY_ATTACK_HOLD_TIME = 1f;

    private float timeHeldForHeavyAttack = 3;

    private bool canPerformHeavyAttack = true;

    public bool Blocking = false;
    public bool CanBlock = true;

    private bool performedHeavyAttack = false;

    void Start()
    {
        DisableAllAttackBoxes();    
    }

    void Update()
    {
        #region Light and Heavy Attack

        if (Input.GetKeyUp(ATTACK_KEY))
        {
            if (timeHeldForHeavyAttack > 0)
            {
                PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.CANCEL_STRONG_ATTACK, true);

                if (canPerformHeavyAttack) // So that the player doesnt light attack after performing a heavy attack when lifting up the mouse button
                {
                    LightAttack();
                }
            }

            if (!performedHeavyAttack)
            {
                if (timeHeldForHeavyAttack >= ACCEPTED_HEAVY_ATTACK_HOLD_TIME)
                {
                    HeavyAttack();
                }
            } else
            {
                performedHeavyAttack = false;
            }

            timeHeldForHeavyAttack = 0;
            canPerformHeavyAttack = true;
        }

        if(Input.GetKeyDown(ATTACK_KEY))
        {         
            timeHeldForHeavyAttack = 0;
            Blocking = false;
        }

        if (Input.GetKey(ATTACK_KEY))
        {
            timeHeldForHeavyAttack += Time.deltaTime;

            if(timeHeldForHeavyAttack > TIME_UNTIL_ATTACK_BUILDUP)
            {
                if(canPerformHeavyAttack)
                {
                    PlayerManager.Instance.Anim.SetTrigger(PlayerAnimationConstants.STRONG_ATTACK_BUILDUP);
                    canPerformHeavyAttack = false;
                }        
            }

            if(timeHeldForHeavyAttack > TIME_UNTIL_HEAVY_ATTACK)
            { 
                if(!performedHeavyAttack)
                {
                    HeavyAttack();
                }           
            }                       
        }

        if (attackSpeed > 0)
        {
            lightMeleeAttackBox.SetActive(true);
            attacking = true;
            attackSpeed -= Time.deltaTime;
        } else
        {
            lightMeleeAttackBox.SetActive(false);
            attacking = false;
        }

        timeSinceLastHit += Time.deltaTime;

        if(timeSinceLastHit > TIME_TO_ATTACK_UNTIL_RESET) // Reset attack animation to start if the player hasnt attacked within the last second
        {
            attackIndex = 0;
        }

        #endregion

        #region Blocking
        if(CanBlock)
        {
            if (Input.GetKeyDown(BLOCK_KEY))
            {
                if(PlayerManager.Instance.PlayerMovement.CanControlPlayer)
                {
                    if (PlayerManager.Instance.PlayerMovement.Grounded())
                    {
                        Blocking = true;
                    }
                }                         
            }       
        }

        if (Input.GetKeyUp(BLOCK_KEY))
        {
            Blocking = false;
            PlayerManager.Instance.PlayerMovement.enabled = true;
        }

        if (Blocking)
        {
            PlayerManager.Instance.PlayerMovement.enabled = false;
        } 

        PlayerManager.Instance.Anim.SetBool(PlayerAnimationConstants.BLOCKING, Blocking);
        #endregion

        #region Body Slam
        if(Input.GetKeyDown(BODY_SLAM_KEY))
        {
            if(!PlayerManager.Instance.PlayerMovement.Grounded())
            {
                PlayerManager.Instance.Anim.SetTrigger(PlayerAnimationConstants.BODY_SLAM);
                PlayerManager.Instance.PlayerMovement.CanControlPlayer = false; 
                PlayerManager.Instance.PlayerMelee.enabled = false;
                PlayerManager.Instance.PlayerMovement.GravityScale = PlayerManager.Instance.PlayerMovement.BodySlamGravityScale;
                StartCoroutine(ShowAttackBox(bodySlamMeleeAttackBox, 1.5f));
            }
        }
        #endregion
    }
    
    IEnumerator ShowAttackBox(GameObject _boxToSetActive, float _timeToWait = 0.5f)
    {
        _boxToSetActive.SetActive(true);
        yield return new WaitForSeconds(_timeToWait);
        _boxToSetActive.SetActive(false);
    }

    void LightAttack()
    {
        if (!PauseMenu.Instance.Paused)
        {
            if (!DialogueManager.Instance.InDialogue)
            {
                if (!PlayerWeapon.Instance.Aiming)
                {
                    if (!PlayerManager.Instance.PlayerLadderClimb.CanClimb)
                    {
                        if (!attacking)
                        {
                            if (PlayerManager.Instance.PlayerMovement.Grounded())
                            {
                                if (PlayerManager.Instance.PlayerMovement.CanControlPlayer)
                                {
                                    if (attackIndex == NUM_OF_ATTACK_ANIMATIONS)
                                    {
                                        attackIndex = 0;
                                    }

                                    Blocking = false;
                                    attackIndex++;
                                    PlayerManager.Instance.Anim.SetTrigger(PlayerAnimationConstants.ATTACK + attackIndex.ToString());
                                    timeSinceLastHit = 0;
                                    attackSpeed = startAttackSpeed;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void HeavyAttack()
    {
        if (!PlayerManager.Instance.Stunned)
        {
            if (!PauseMenu.Instance.Paused)
            {
                if (!DialogueManager.Instance.InDialogue)
                {
                    if (!PlayerManager.Instance.PlayerLadderClimb.CanClimb)
                    {
                        if (!attacking)
                        {
                            if (PlayerManager.Instance.PlayerMovement.Grounded())
                            {
                                if (PlayerManager.Instance.PlayerMovement.CanControlPlayer)
                                {
                                    Blocking = false;
                                    PlayerManager.Instance.Anim.SetTrigger(PlayerAnimationConstants.STRONG_ATTACK);
                                    StartCoroutine(ShowAttackBox(heavyMeleeAttackBox));
                                    timeHeldForHeavyAttack = 0;
                                    performedHeavyAttack = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void DisableAllAttackBoxes()
    {
        lightMeleeAttackBox.SetActive(false);
        heavyMeleeAttackBox.SetActive(false);
        bodySlamMeleeAttackBox.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyLightAttack"))
        {         
            if (!PlayerManager.Instance.PlayerMelee.Blocking)
            {
                PlayerHealth.Instance.TakeDamage(other.GetComponentInParent<EnemyManager>().LightAttackStrength, true);
            }
            else
            {
                PlayerHealth.Instance.TakeDamage(other.GetComponentInParent<EnemyManager>().LightAttackStrength * 0.5f, true);
                PlayerManager.Instance.Anim.SetTrigger(PlayerAnimationConstants.BLOCK_HIT);
            }
        }

        if (other.gameObject.CompareTag("EnemyHeavyAttack"))
        {
            if (!PlayerManager.Instance.PlayerMelee.Blocking)
            {
                PlayerHealth.Instance.TakeDamage(other.GetComponentInParent<EnemyManager>().HeavyAttackStrength, false);
            }
            else
            {
                PlayerHealth.Instance.TakeDamage(other.GetComponentInParent<EnemyManager>().HeavyAttackStrength * 0.5f, false);
                PlayerManager.Instance.Anim.SetTrigger(PlayerAnimationConstants.BLOCK_HIT);
            }
        }
    }
}
