using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SettingsApply : MonoBehaviour
{
    [SerializeField] private GameObject postProcessingObject = null;

    public static SettingsApply Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }    
    }

    void Start()
    {
        if (postProcessingObject == null)
        {
            Debug.LogError("There is no Post Processing Object!");
        } else
        {
            ApplySettings();
        }

        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(PlayerPrefConstants.QUALITY_SETTING, 2));
    }

    public void ApplySettings()
    {
        // Should post processing be enabled?
        if (PlayerPrefs.GetInt(PlayerPrefConstants.POST_PROCESSING_SETTING, 1) == 0)
        {
            postProcessingObject.SetActive(false);
        }
        else
        {
            postProcessingObject.SetActive(true);
        }   
    }
}
