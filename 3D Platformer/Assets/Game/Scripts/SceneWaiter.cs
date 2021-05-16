using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWaiter : MonoBehaviour
{
    [SerializeField] private float sceneTime;
    [SerializeField] private SceneManagement sceneManager;
    [SerializeField] private string sceneToLoad;

    void Update()
    {  
        if (sceneTime < 0)
        {
            sceneManager.LoadScene(sceneToLoad);
        } else
        {
            sceneTime -= Time.deltaTime;
        }
    }
}
