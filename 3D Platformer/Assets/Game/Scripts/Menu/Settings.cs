using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    #region Constants
    public const string VSYNC_SETTING = "VSyncSetting";
    public const string FULLSCREEN_SETTING = "FullScreenSetting";
    public const string MOTION_BLUR_SETTING = "MotionBlurSetting";
    public const string FILM_GRAIN_SETTING = "FilmGrainSetting";

    public const string MUSIC_VOLUME_SETTING = "MusicVolumeSetting";
    public const string VOICE_VOLUME_SETTING = "VoiceVolumeSetting";

    //public const string RESOLUTION_SETTING = "ResolutionSetting";
    public const string QUALITY_SETTING = "QualitySetting";
    #endregion

    #region References
    [Header("Toggles")]
    [SerializeField] private Toggle vSyncToggle;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle motionBlurToggle;
    [SerializeField] private Toggle filmGrainToggle;

    [Header("Sliders")]
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider voiceVolumeSlider;

    [Header("Dropdowns")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    #endregion

    private Resolution[] resolutions;


    void OnEnable()
    {
        #region Resolution Set

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int _currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                _currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        #endregion

        #region Setting Settings Values

        vSyncToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(VSYNC_SETTING));
        fullscreenToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(FULLSCREEN_SETTING));
        motionBlurToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(MOTION_BLUR_SETTING));
        filmGrainToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt(FILM_GRAIN_SETTING));

        musicVolumeSlider.value = PlayerPrefs.GetFloat(MUSIC_VOLUME_SETTING);
        voiceVolumeSlider.value = PlayerPrefs.GetFloat(VOICE_VOLUME_SETTING);

        resolutionDropdown.value = _currentResolutionIndex;


        qualityDropdown.value = PlayerPrefs.GetInt(QUALITY_SETTING);

        #endregion

        resolutionDropdown.RefreshShownValue();
    }

    public void SetVSync(bool setVsync) // Set whether vSync should be enabled or disabled
    {
        QualitySettings.vSyncCount = Convert.ToInt16(setVsync); // Set vSync to whatever the setting is currently at
        PlayerPrefs.SetInt(VSYNC_SETTING, Convert.ToInt16(setVsync));
    }

    public void SetFullScreen(bool _isFullscreen) // Set whether the game should be fullscreen or not
    {
        Screen.fullScreen = _isFullscreen;
        PlayerPrefs.SetInt(FULLSCREEN_SETTING, Convert.ToInt16(_isFullscreen));
    }

    public void SetMotionBlur(bool _setMotionBlur) // Set whether motion blur should be enabled or disabled
    {
        // Enable/disable motion blur
        PlayerPrefs.SetInt(MOTION_BLUR_SETTING, Convert.ToInt16(_setMotionBlur));

        // Debug.Log(Convert.ToInt16(setMotionBlur).ToString());
    }

    public void SetFilmGrain(bool _setFilmGrain) // Set whether motion blur should be enabled or disabled
    {
        // Enable/disable film grain
        PlayerPrefs.SetInt(FILM_GRAIN_SETTING, Convert.ToInt16(_setFilmGrain));
    }

    public void SetMusicVolue(float _value) // Set the volume of the music
    {
        // Set the music volume to the _value setting
        PlayerPrefs.SetFloat(MUSIC_VOLUME_SETTING, _value);
    }
    public void SetVoiceVolume(float _value) // Set the volume of the music
    {
        // Set the voice volume to the _value setting
        PlayerPrefs.SetFloat(VOICE_VOLUME_SETTING, _value);
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
        PlayerPrefs.SetInt(QUALITY_SETTING, _qualityIndex);
    }
}
