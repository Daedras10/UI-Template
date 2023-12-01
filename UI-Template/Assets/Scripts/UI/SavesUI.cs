using System.Collections;
using System.Collections.Generic;
using Managers;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

public class SavesUI : ToolkitCanvasUI
{
    protected override void Init()
    {
        root.style.visibility = Visibility.Hidden;
        
        // Close Settings on callback
        var closeBg = root.Q<VisualElement>("CloseBg");
        closeBg.RegisterCallback<ClickEvent>(ev =>Hide());
            
        // Block propagation
        var settingContainer = root.Q<VisualElement>("SettingContainer");
        settingContainer.RegisterCallback<ClickEvent>(ev => ev.StopPropagation());
        
        // TODO : buttons to start game
        // TODO : Init saves
        SetupButton("Save1", "Save 1");
        SetupButton("Save2", "Save 2");
    }
    
    private void SetupButton(string tmplName, string slotName)
    {
        var tmplRoot = root.Q<VisualElement>(tmplName);

        var button = tmplRoot.Q<Button>("NewGameB");
        button.RegisterCallback<ClickEvent>(ev => LoadGame() );
        
        var slot = tmplRoot.Q<TextField>("SlotName"); 
        slot.value = slotName;
    }
    
    private void LoadGame()
    {
        GameManager.instance.LoadGame();
    }

    public void Show()
    {
        root.style.visibility = Visibility.Visible;
    }
    
    public void Hide()
    {
        root.style.visibility = Visibility.Hidden;
    }

    protected override void UpdateLanguage()
    {
        var tmplRoot = root.Q<VisualElement>("Save1");
        var button = tmplRoot.Q<Button>("NewGameB");
        button.text = JsonLoader.GetChoosenUitranslation("NewGameB");
        
        tmplRoot = root.Q<VisualElement>("Save2");
        button = tmplRoot.Q<Button>("NewGameB");
        button.text = JsonLoader.GetChoosenUitranslation("NewGameB");
        
        root.Q<Label>("Settings_tile").text = JsonLoader.GetChoosenUitranslation("SaveTitle");
    }
}
