using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class MainMenu : UIToolkitCanvas
    {
        [SerializeField] private UISettings uiSettings;
        [SerializeField] private bool playerHasSaves = false;
        
        private Button settingsButton;
        
        protected override void Init()
        {
            settingsButton = GetButtonFromTmplInst("SettingsB");
            Debug.Log($"{settingsButton}");
            settingsButton.RegisterCallback<ClickEvent>(ev => uiSettings.ShowSettings());
            
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
