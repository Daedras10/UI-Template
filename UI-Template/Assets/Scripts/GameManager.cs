using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private static string mainMenuSceneName = "MainMenu";
    private static string gameSceneName = "Gaming";
    
    public static GameManager instance;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuSceneName);
    }
    
    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameSceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSettings()
    {
        // Load settings from playerprefs
    }
    
    public void SaveSettings()
    {
        // Save settings to playerprefs
    }
    
    
}
