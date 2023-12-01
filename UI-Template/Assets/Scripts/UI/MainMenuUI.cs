using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenuUI : ToolkitCanvasUI
    {
        [SerializeField] private SettingsUI settingsUI;
        [SerializeField] private SavesUI savesUI;
        [SerializeField] private bool playerHasSaves = false;
        
        private Button settingsButton;
        private Button playButton;
        private Button quitButton;
        private Button loadButton;
        
        protected override void Init()
        {
            settingsButton = GetButtonFromTmplInst("SettingsB");
            settingsButton.RegisterCallback<ClickEvent>(ev => settingsUI.ShowSettings());
            
            playButton = GetButtonFromTmplInst("PlayB");
            playButton.RegisterCallback<ClickEvent>(ev => savesUI.Show());
            
            loadButton = GetButtonFromTmplInst("LoadB");
            
            quitButton = GetButtonFromTmplInst("QuitB");
            quitButton.RegisterCallback<ClickEvent>(ev => Application.Quit() );
            
            if (!playerHasSaves)
            {
                // Disable Continue button
                var continueButton = GetButtonFromTmplInst("LoadB");
                continueButton.SetEnabled(false);
            }
        }
        
        private Button GetButtonFromTmplInst(string name)
        {
            // Settings button (in template instance so we can't use Q<Button>("SettingsB"))
            return (Button)root.Q<VisualElement>(name).Children().First();
        }

        protected override void UpdateLanguage()
        {
            playButton.text = JsonLoader.GetChoosenUitranslation("PlayB");
            loadButton.text = JsonLoader.GetChoosenUitranslation("LoadB");
            settingsButton.text = JsonLoader.GetChoosenUitranslation("SettingsB");
            quitButton.text = JsonLoader.GetChoosenUitranslation("QuitB");
        }
    }
}
