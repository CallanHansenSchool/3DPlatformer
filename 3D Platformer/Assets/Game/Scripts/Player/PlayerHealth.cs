using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour // Manages the players health
{
    public float CurrentHealth = 0;
    [SerializeField] private float maxHealth = 6.0f;

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
        HUD.Instance.HealthBarSlider.maxValue = maxHealth;    
        HUD.Instance.HealthBarSlider.minValue = 0;

        ResetHealth();
    }

    public void ResetHealth()
    {
        CurrentHealth = maxHealth;
        HUD.Instance.UpdateHUD();
    }

    public void CheckHealth()
    {
        if(CurrentHealth <= 0) // Is the player dead?
        {
            PlayerManager.Instance.StartCoroutine(PlayerManager.Instance.BeginDeath()); // Kill the player
        }
    }

    public void TakeDamage(float _damageToTake)
    {
        CurrentHealth -= _damageToTake;
        CheckHealth();
        HUD.Instance.UpdateHUD();
    }
}
