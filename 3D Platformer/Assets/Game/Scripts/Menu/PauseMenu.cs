using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu = null;

    private bool paused = false;

    void Start()
    {
        pauseMenu.gameObject.SetActive(false);    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        paused = !paused;

        if (paused)
        {
            CameraZoom.Instance.CanZoom = false;
            Time.timeScale = 0;
            pauseMenu.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            CameraZoom.Instance.CanZoom = true;
            Time.timeScale = 1;
            pauseMenu.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}