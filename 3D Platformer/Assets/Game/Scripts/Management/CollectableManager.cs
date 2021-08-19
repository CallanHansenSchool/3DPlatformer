using UnityEngine;

public class CollectableManager : MonoBehaviour // For managing the collectable types
{
    public int CommonCollectablesCollected = 0;
    public int RareCollectablesCollected = 0;

    public const int COMMON_COLLECTABLES_NEEDED_FOR_LIFE = 100;

    public static CollectableManager Instance;

    private float timeSinceLastCollectable = 0;

    private bool comboStarted;

    void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    void Update()
    {
        if(comboStarted)
        {
            if (timeSinceLastCollectable < 0)
            {
                comboStarted = false;
       
            }
            else
            {
                timeSinceLastCollectable -= Time.deltaTime;
            }
        }      
    }
    
    public void CheckCollectables()
    {
        if(CommonCollectablesCollected >= COMMON_COLLECTABLES_NEEDED_FOR_LIFE)
        { 
            PlayerPrefs.SetInt(PlayerPrefConstants.PLAYER_LIVES, PlayerPrefs.GetInt(PlayerPrefConstants.PLAYER_LIVES) + 1); // Add a life
            HUD.Instance.UpdateHUD();
            CommonCollectablesCollected -= COMMON_COLLECTABLES_NEEDED_FOR_LIFE;
            comboStarted = true;
        }
    }
}
