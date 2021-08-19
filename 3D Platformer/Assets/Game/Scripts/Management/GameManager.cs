using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public Camera MainCamera = null;

    [Header("Virtual Cameras")]
    public CinemachineFreeLook MainVirtualCamera = null;
    public CinemachineFreeLook aimingCamera = null;

    public static GameManager Instance;

    #region Tags
    public const string PLAYER_TAG = "Player";
    public const string ENEMY_TAG = "Enemy";
    public const string CLIMBABLE_TAG = "Climbable";
    public const string WATER_TAG = "Water";
    public const string FALL_PIT = "Fallpit";
    public const string GROUND_TAG = "Ground";
    #endregion

    #region SceneNames
    public const string MAIN_MENU = "MainMenu";
    public const string CREDITS = "Credits";
    #endregion

    public GameObject[] RareCollectables = null;
    
    public bool UsingController = false;

    void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion

        //DebugResetPlayerPrefs();
    }

    void Start()
    {       
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    void DebugResetPlayerPrefs() // For resetting all playerprefs
    {
        PlayerPrefs.DeleteAll();
    }


  
}