using System;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public class JsonLoader : MonoBehaviour
    {
        [SerializeField] private TextAsset jsonFileChoice;
        [SerializeField] private TextAsset jsonFileDialog;
        [SerializeField] private TextAsset jsonFileUI;
        
        [Serializable] public class AllDialogs
        {
            public Dialog[] dialog;

            public Dialog GetDialog(string id)
            {
                return dialog.First(d => d.id == id);
            }
        }
        
        [Serializable] public class Dialog
        {
            public string id;
            public string talkerfr;
            public string talkeren;
            public string textfr;
            public string texten;
            public int sideleft;
        }
        
        [Serializable] public class AllChoices
        {
            public Choice[] choice;

            public Choice GetChoice(string id)
            {
                return choice.First(c => c.id == id);
            }
        }
        
        [Serializable] public class Choice
        {
            public string id;
            public string textfr;
            public string texten;
            public string nextdialog;
        }
        
        [Serializable] public class AllUitranslations
        {
            public Uitranslation[] uitranslation;

            public Uitranslation GetUitranslation(string id)
            {
                return uitranslation.First(u => u.id == id);
            }
        }
        
        [Serializable] public class Uitranslation
        {
            public string id;
            public string textfr;
            public string texten;
        }
        
        [ContextMenu("Read Jsons")] 
        public void ReadJsons()
        {
            GameData.allDialogs = new AllDialogs();
            GameData.allDialogs = JsonUtility.FromJson<AllDialogs>(jsonFileDialog.text);
            
            GameData.allChoices = new AllChoices();
            GameData.allChoices = JsonUtility.FromJson<AllChoices>(jsonFileChoice.text);
            
            GameData.allUitranslations = new AllUitranslations();
            GameData.allUitranslations = JsonUtility.FromJson<AllUitranslations>(jsonFileUI.text);
        }
        
        public static Dialog GetDialog(string id)
        {
            return GameData.allDialogs.GetDialog(id);
        }
        
        public static Choice GetChoice(string id)
        {
            return GameData.allChoices.GetChoice(id);
        }
        
        public static Uitranslation GetUitranslation(string id)
        {
            return GameData.allUitranslations.GetUitranslation(id);
        }

        public static string GetChoosenUitranslation(string id)
        {
            var translation = GetUitranslation(id);
            switch (GameData.chosenLanguage)
            {
                case ChosenLanguage.English:
                    return translation.texten;
                case ChosenLanguage.French:
                    return translation.textfr;
                default:
                    return translation.texten;
            }
        }
        
        public static string GetChoosenDialog(string id)
        {
            var dialog = GetDialog(id);
            switch (GameData.chosenLanguage)
            {
                case ChosenLanguage.English:
                    return dialog.texten;
                case ChosenLanguage.French:
                    return dialog.textfr;
                default:
                    return dialog.texten;
            }
        }
        
        public static string GetChoosenChoice(string id)
        {
            var choice = GetChoice(id);
            switch (GameData.chosenLanguage)
            {
                case ChosenLanguage.English:
                    return choice.texten;
                case ChosenLanguage.French:
                    return choice.textfr;
                default:
                    return choice.texten;
            }
        }
        
    }
}