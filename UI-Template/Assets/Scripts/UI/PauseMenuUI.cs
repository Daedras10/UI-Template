using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class PauseMenuUI : ToolkitCanvasUI
    {
        protected override void Init()
        {
            root.style.visibility = Visibility.Hidden;
            
            root.Q<Button>("SettingsB").RegisterCallback<ClickEvent>(ev => Debug.Log("Settings")); //TODO
            root.Q<Button>("ResumeB").RegisterCallback<ClickEvent>(ev => root.style.visibility = Visibility.Hidden);
            root.Q<Button>("MainMenuB").RegisterCallback<ClickEvent>(ev => GameManager.instance.LoadMainMenu() );
            root.Q<Button>("QuitB").RegisterCallback<ClickEvent>(ev => Debug.Log("Quit"));
            
        }
    }
}