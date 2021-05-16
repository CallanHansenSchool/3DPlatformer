using UnityEngine;

public class PostProcessingObject : MonoBehaviour // Ensures that 
{
    public static PostProcessingObject Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }
}
