using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private static string mainMenuSceneName = "MainMenu";
        private static string gameSceneName = "Gaming";
    
        public static event Action OnSettingsChanged;
        public static event Action OnLanguageChanged;
    
        public static GameManager instance;
    
        public PlayerInputs playerInputs;
    
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private JsonLoader jsonLoader;
        
        private ChosenLanguage previousChosenLanguage;
    
        // Start is called before the first frame update
        void Start()
        {
            if (instance == null)
            {
                instance = this;
                playerInputs = new PlayerInputs();
                playerInputs.Enable();
                if (PlayerPrefs.HasKey("volume")) LoadSettings();
                jsonLoader.ReadJsons();
            
                ApplySettings();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
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
            GameData.volume = PlayerPrefs.GetFloat("volume", 1f);
            GameData.soundMuted = PlayerPrefs.GetInt("soundMuted", 0) == 1;
            GameData.cubeScaleMin = PlayerPrefs.GetFloat("cubeScaleMin", 1f);
            GameData.cubeScaleMax = PlayerPrefs.GetFloat("cubeScaleMax", 3f);
            GameData.chosenLanguage = GameData.StringToLanguage(PlayerPrefs.GetString("chosenLanguage", "English"));

            string colorString = PlayerPrefs.GetString("cubeColor", "red");
        
            if (colorString == "red") GameData.cubeColor = Color.red;
            else if (colorString == "green") GameData.cubeColor = Color.green;
            else if (colorString == "blue") GameData.cubeColor = Color.blue;
            else GameData.cubeColor = Color.white;

        }
    
        public void SaveSettings()
        { 
            // Save settings to playerprefs
            PlayerPrefs.SetFloat("volume", GameData.volume);
            PlayerPrefs.SetInt("soundMuted", GameData.soundMuted ? 1 : 0);
            PlayerPrefs.SetFloat("cubeScaleMin", GameData.cubeScaleMin);
            PlayerPrefs.SetFloat("cubeScaleMax", GameData.cubeScaleMax);
            PlayerPrefs.SetString("cubeColor", GameData.cubeColor.ToString());
            PlayerPrefs.SetString("chosenLanguage", GameData.LanguageToString(GameData.chosenLanguage) );
        }

        public void ApplySettings()
        {
            SaveSettings();
        
            if (audioSource == null)
            {
                audioSource = Camera.main.GetComponent<AudioSource>();
            }

            audioSource.volume = GameData.volume;
            audioSource.mute = GameData.soundMuted;
        
            OnSettingsChanged?.Invoke();
            
            if (previousChosenLanguage != GameData.chosenLanguage)
            {
                previousChosenLanguage = GameData.chosenLanguage;
                OnLanguageChanged?.Invoke();
            }
        }
    }
    
    public enum ChosenLanguage
    {
        English,
        French
    }
}
