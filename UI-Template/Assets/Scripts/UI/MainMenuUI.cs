using System.Linq;
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
        
        protected override void Init()
        {
            settingsButton = GetButtonFromTmplInst("SettingsB");
            settingsButton.RegisterCallback<ClickEvent>(ev => settingsUI.ShowSettings());
            
            playButton = GetButtonFromTmplInst("PlayB");
            playButton.RegisterCallback<ClickEvent>(ev => savesUI.Show());
            
            
            if (!playerHasSaves) // TODO : Check if player has saves
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
    }
}
