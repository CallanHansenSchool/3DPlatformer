using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void NewGame() // Create a new game
    {
        // Edit save files and load into new scene
    }

    public void LoadGame() // Load into the previously saved game
    {
        
    }

    public void LoadScene(string _sceneToLoad) // Open a new scene
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
    
    public void QuitGame() // Exit the application
    {
        Application.Quit();
    }
}
