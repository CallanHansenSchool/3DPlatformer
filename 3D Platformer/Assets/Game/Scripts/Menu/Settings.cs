using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class Settings : MonoBehaviour
{
    #region References
    [Header("Toggles")]
    [SerializeField] private Toggle vSyncToggle = null;
    [SerializeField] private Toggle fullscreenToggle = null;
    [SerializeField] private Toggle postProcessingToggle = null;
    [SerializeField] private Toggle grassEnabledToggle = null;

    [Header("Sliders")]
    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private Slider voiceVolumeSlider = null;

    [Header("Dropdowns")]
    [SerializeField] private TMP_Dropdown resolutionDropdown = null;
    [SerializeField] private TMP_Dropdown qualityDropdown = null;
    [SerializeField] private TMP_Dropdown grassDetailDropdown = null;
    #endregion
    
    [SerializeField] private Terrain lowDetailTerrain = null;
    [SerializeField] private Terrain standardDetailTerrain = null;
    [SerializeField] private Terrain highDetailTerrain = null;

    private Resolution[] resolutions;

    void Awake()
    {
        SetTerrain(PlayerPrefs.GetInt(PlayerPrefConstants.TERRAIN_DETAIL));

        #region Resolution Set

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int _currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @" + resolutions[i].refreshRate + "Hz";

            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                _currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        #endregion

        #region Setting Settings Values

        vSyncToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(PlayerPrefConstants.VSYNC_SETTING));
        fullscreenToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(PlayerPrefConstants.FULLSCREEN_SETTING, 1));
        postProcessingToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(PlayerPrefConstants.POST_PROCESSING_SETTING, 1));

        grassEnabledToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(PlayerPrefConstants.GRASS_SETTING, 1));
        SetGrass(Convert.ToBoolean(PlayerPrefs.GetInt(PlayerPrefConstants.GRASS_SETTING, 1)));
     
        musicVolumeSlider.value = PlayerPrefs.GetFloat(PlayerPrefConstants.MUSIC_VOLUME_SETTING);
        voiceVolumeSlider.value = PlayerPrefs.GetFloat(PlayerPrefConstants.VOICE_VOLUME_SETTING);

        resolutionDropdown.value = _currentResolutionIndex;

        qualityDropdown.value = PlayerPrefs.GetInt(PlayerPrefConstants.QUALITY_SETTING, 2);

        grassDetailDropdown.value = PlayerPrefs.GetInt(PlayerPrefConstants.TERRAIN_DETAIL, 1);

        #endregion

        resolutionDropdown.RefreshShownValue();
    }

    public void SetTerrain(int _terrainQualityIndex)
    {
        PlayerPrefs.SetInt(PlayerPrefConstants.TERRAIN_DETAIL, _terrainQualityIndex);

        switch (PlayerPrefs.GetInt(PlayerPrefConstants.TERRAIN_DETAIL, 1))
        {
            case 0:
                lowDetailTerrain.gameObject.SetActive(true);
                standardDetailTerrain.gameObject.SetActive(false);
                highDetailTerrain.gameObject.SetActive(false);
                break;

            case 1:
                lowDetailTerrain.gameObject.SetActive(false);
                standardDetailTerrain.gameObject.SetActive(true);
                highDetailTerrain.gameObject.SetActive(false);
                break;

            case 2:
                lowDetailTerrain.gameObject.SetActive(false);
                standardDetailTerrain.gameObject.SetActive(false);
                highDetailTerrain.gameObject.SetActive(true);
                break;
        }
    }

    public void SetGrass(bool _setGrass)
    {
        PlayerPrefs.SetInt(PlayerPrefConstants.GRASS_SETTING, Convert.ToInt16(_setGrass));
        lowDetailTerrain.drawTreesAndFoliage = _setGrass;
        standardDetailTerrain.drawTreesAndFoliage = _setGrass;
        highDetailTerrain.drawTreesAndFoliage = _setGrass;
    }

    public void SetVSync(bool _setVsync) // Set whether vSync should be enabled or disabled
    {
        QualitySettings.vSyncCount = Convert.ToInt16(_setVsync); // Set vSync to whatever the setting is currently at
        PlayerPrefs.SetInt(PlayerPrefConstants.VSYNC_SETTING, Convert.ToInt16(_setVsync));       
    }

    public void SetFullScreen(bool _isFullscreen) // Set whether the game should be fullscreen or not
    {
        Screen.fullScreen = _isFullscreen;
        PlayerPrefs.SetInt(PlayerPrefConstants.FULLSCREEN_SETTING, Convert.ToInt16(_isFullscreen));
    }

    public void SetPostProcessing(bool _setPostProcessing) // Set whether post processing should be enabled or disabled
    {
        PlayerPrefs.SetInt(PlayerPrefConstants.POST_PROCESSING_SETTING, Convert.ToInt16(_setPostProcessing));
        SettingsApply.Instance.ApplySettings();
    }

    public void SetMusicVolue(float _value) // Set the volume of the music
    {
        // Set the music volume to the _value setting
        PlayerPrefs.SetFloat(PlayerPrefConstants.MUSIC_VOLUME_SETTING, _value);
    }
    public void SetVoiceVolume(float _value) // Set the volume of the music
    {
        // Set the voice volume to the _value setting
        PlayerPrefs.SetFloat(PlayerPrefConstants.VOICE_VOLUME_SETTING, _value);
    }

    public void SetResolution(int _resolutionIndex) // Set the resolution that the game runs at
    {
        Resolution _resolution = resolutions[_resolutionIndex];
        Screen.SetResolution(_resolution.width, _resolution.height, Screen.fullScreen);
       // PlayerPrefs.SetInt(RESOLUTION_SETTING, _resolutionIndex);
    }

    public void SetQuality(int _qualityIndex) // Set the quality of the game
    {
        QualitySettings.SetQualityLevel(_qualityIndex);
        PlayerPrefs.SetInt(PlayerPrefConstants.QUALITY_SETTING, _qualityIndex);
    }
}
