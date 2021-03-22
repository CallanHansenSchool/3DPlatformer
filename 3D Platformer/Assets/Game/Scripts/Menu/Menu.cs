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
        SceneManagement.Instance.LoadScene(_sceneToLoad);
    }

    public void ReloadCurrentScene()
    {
        SceneManagement.Instance.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void QuitGame() // Exit the application
    {
        Application.Quit();
    }
}
