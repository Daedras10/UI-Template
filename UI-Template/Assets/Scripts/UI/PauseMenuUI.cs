using System;
using Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class PauseMenuUI : ToolkitCanvasUI
    {
        [SerializeField] private SettingsUI settingsUI;
        
        private Button settingsButton;
        private Button resumeButton;
        private Button mainMenuButton;
        private Button quitButton;
        
        protected override void Init()
        {
            root.style.visibility = Visibility.Hidden;
            // PlayerInput escape += Show;
            
            settingsButton = root.Q<Button>("SettingsB");
            resumeButton = root.Q<Button>("ResumeB");
            mainMenuButton = root.Q<Button>("MainMenuB");
            quitButton = root.Q<Button>("QuitB");
            
            settingsButton.RegisterCallback<ClickEvent>(ev => settingsUI.ShowSettings() );
            resumeButton.RegisterCallback<ClickEvent>(ev => root.style.visibility = Visibility.Hidden);
            mainMenuButton.RegisterCallback<ClickEvent>(ev => GameManager.instance.LoadMainMenu() );
            quitButton.RegisterCallback<ClickEvent>(ev => Application.Quit() );
        }
        
        public void Show()
        {
            root.style.visibility = Visibility.Visible;
        }
        
        public void Hide()
        {
            root.style.visibility = Visibility.Hidden;
        }
        
        private void ShowOrHideMenu(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (root.style.visibility == Visibility.Visible)
            {
                Hide();
                settingsUI.HideSettings();
            }
            else
            {
                Show();
            }
        }

        private void OnEnable()
        {
            GameManager.instance.playerInputs.Player.OpenMenu.performed += ShowOrHideMenu;
        }
        
        private void OnDisable()
        {
            GameManager.instance.playerInputs.Player.OpenMenu.performed -= ShowOrHideMenu;
        }
        
        protected override void UpdateLanguage()
        {
            settingsButton.text = JsonLoader.GetChoosenUitranslation("SettingsB");
            resumeButton.text = JsonLoader.GetChoosenUitranslation("ResumeB");
            mainMenuButton.text = JsonLoader.GetChoosenUitranslation("MainMenuB");
            quitButton.text = JsonLoader.GetChoosenUitranslation("QuitB");
        }
    }
}