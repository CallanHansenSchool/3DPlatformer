using UnityEngine;

public class CollectableManager : MonoBehaviour // For managing the collectable types
{
    public int CommonCollectablesCollected = 0;
    public int RareCollectablesCollected = 0;

    private const int COMMON_COLLECTABLES_NEEDED_FOR_LIFE = 100;

    public static CollectableManager Instance;

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

    public void CheckCollectables()
    {
        if(CommonCollectablesCollected >= COMMON_COLLECTABLES_NEEDED_FOR_LIFE)
        { 
            PlayerPrefs.SetInt(PlayerPrefConstants.PLAYER_LIVES, PlayerPrefs.GetInt(PlayerPrefConstants.PLAYER_LIVES) + 1); // Add a life
            HUD.Instance.UpdateHUD();
            CommonCollectablesCollected -= COMMON_COLLECTABLES_NEEDED_FOR_LIFE;
        }
    }
}
