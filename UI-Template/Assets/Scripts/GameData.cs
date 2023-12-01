using Managers;
using UnityEngine;

public class GameData
{
    public static bool dataLoaded = false;
    
    public static float volume = 1;
    public static bool soundMuted = false;

    public static float cubeScaleMin = 1;
    public static float cubeScaleMax = 3;
    
    public static Color cubeColor = Color.red;
    public static ChosenLanguage chosenLanguage = ChosenLanguage.English;
    
    
    /* JsonDatas */
    public static JsonLoader.AllDialogs allDialogs = new JsonLoader.AllDialogs();
    public static JsonLoader.AllChoices allChoices = new JsonLoader.AllChoices();
    public static JsonLoader.AllUitranslations allUitranslations = new JsonLoader.AllUitranslations();
    
    
    public static string LanguageToString(ChosenLanguage language)
    {
        switch (language)
        {
            case ChosenLanguage.English:
                return "English";
            case ChosenLanguage.French:
                return "French";
            default:
                return "English";
        }
    }
    
    public static ChosenLanguage StringToLanguage(string language)
    {
        switch (language)
        {
            case "English":
                return ChosenLanguage.English;
            case "French":
                return ChosenLanguage.French;
            default:
                return ChosenLanguage.English;
        }
    }
}
