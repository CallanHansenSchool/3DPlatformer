using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour // Manages the players health
{
    public float CurrentHealth = 0;
    public float MaxHealth = 6.0f;

    public static PlayerHealth Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HUD.Instance.HealthBarSlider.maxValue = MaxHealth;    
        HUD.Instance.HealthBarSlider.minValue = 0;

        ResetHealth();
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
        HUD.Instance.UpdateHUD();
    }

    public void CheckHealth()
    {
        if(CurrentHealth <= 0) // Is the player dead?
        {
            PlayerManager.Instance.StartCoroutine(PlayerManager.Instance.BeginDeath()); // Kill the player
        }
    } 

    public void TakeDamage(float _damageToTake, bool _lightAttack = false)
    {
        if (!PlayerManager.Instance.PlayerMelee.Blocking)
        {
            if(!PlayerManager.Instance.Dead)
            {
                if (PlayerManager.Instance.PlayerMovement.CanControlPlayer)
                {
                    if (_lightAttack)
                    {
                        PlayerManager.Instance.Anim.SetTrigger(PlayerAnimationConstants.LIGHT_ATTACK_HIT_REACTION);
                        AudioManager.Instance.PlayAudio("PlayerLightAttackHitReaction");
                        Debug.Log("Hit with light attack");
                    }
                    else
                    {
                        PlayerManager.Instance.Anim.SetTrigger(PlayerAnimationConstants.HEAVY_ATTACK_HIT_REACTION);
                        AudioManager.Instance.PlayAudio("PlayerHeavyAttackHitReaction");
                        Debug.Log("Hit with heavy attack");
                    }
                }               
            }          
        }

        CurrentHealth -= _damageToTake;
        CheckHealth();
        HUD.Instance.UpdateHUD();
    }
}
