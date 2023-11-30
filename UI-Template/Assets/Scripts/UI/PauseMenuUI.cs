using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class PauseMenuUI : ToolkitCanvasUI
    {
        [SerializeField] private SettingsUI settingsUI;
        
        
        protected override void Init()
        {
            root.style.visibility = Visibility.Hidden;
            // PlayerInput escape += Show;
            
            root.Q<Button>("SettingsB").RegisterCallback<ClickEvent>(ev => settingsUI.ShowSettings() );
            root.Q<Button>("ResumeB").RegisterCallback<ClickEvent>(ev => root.style.visibility = Visibility.Hidden);
            root.Q<Button>("MainMenuB").RegisterCallback<ClickEvent>(ev => GameManager.instance.LoadMainMenu() );
            root.Q<Button>("QuitB").RegisterCallback<ClickEvent>(ev => Application.Quit() );
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
    }
}