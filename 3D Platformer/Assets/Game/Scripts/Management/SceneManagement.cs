using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance;

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

    public void LoadScene(string _sceneToLoad)
    {
        SceneManager.LoadScene(_sceneToLoad);
    }
}