using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class SettingsUI : ToolkitCanvasUI
    {
        private static string selectedPseudoClass = "SelectedButton";
        
        
        public event Action OnClose;

        private VisualElement closeBg;
        private SettingsTabs currentTab = SettingsTabs.Audio;


        protected override void Init()
        {
            root.style.display = DisplayStyle.None;
            
            // Close Settings on callback
            closeBg = root.Q<VisualElement>("CloseBg");
            closeBg.RegisterCallback<ClickEvent>(ev =>HideSettings());
            
            // Block propagation
            var settingContainer = root.Q<VisualElement>("SettingContainer");
            settingContainer.RegisterCallback<ClickEvent>(ev => ev.StopPropagation());
            
            var audioTab = root.Q<VisualElement>("AudioB").Children().First();
            audioTab.RegisterCallback<ClickEvent>(ev => SelectTab(SettingsTabs.Audio));
            
            var gameplayTab = root.Q<VisualElement>("GameplayB").Children().First();
            gameplayTab.RegisterCallback<ClickEvent>(ev => SelectTab(SettingsTabs.Gameplay));
            
            ShowOnlyFirstTab();
        }
        
        private void ShowOnlyFirstTab()
        {
            // Initial tab selected
            ShowTab(SettingsTabs.Audio);
            
            // Hide other tabs
            HideTab(SettingsTabs.Gameplay);
        }
        
        private void HideTab(SettingsTabs tab)
        {
            root.Q<VisualElement>(GetTabName(tab) + "B").Children().First().RemoveFromClassList(selectedPseudoClass);
            root.Q<VisualElement>(GetTabName(tab) + "Container").style.display = DisplayStyle.None;
        }
        
        private void ShowTab(SettingsTabs tab)
        {
            root.Q<VisualElement>(GetTabName(tab) + "B").Children().First().AddToClassList(selectedPseudoClass);
            root.Q<VisualElement>(GetTabName(tab) + "Container").style.display = DisplayStyle.Flex;
        }
        
        private void SelectTab(SettingsTabs tab)
        {
            if (tab == currentTab)
                return;
            
            HideTab(currentTab);
            ShowTab(tab);
            
            currentTab = tab;
        }

        public void HideSettings()
        {
            root.style.display = DisplayStyle.None;
            OnClose?.Invoke();
        }
        
        public void ShowSettings()
        {
            root.style.display = DisplayStyle.Flex;
        }
        
        public bool IsVisible()
        {
            return root.style.display == DisplayStyle.Flex;
        }
        
        public static string GetTabName(SettingsTabs tab)
        {
            switch (tab)
            {
                case SettingsTabs.Audio:
                    return "Audio";
                case SettingsTabs.Gameplay:
                    return "Gameplay";
                default:
                    return "";
            }
        }
    }
    
    public enum SettingsTabs
    {
        Audio,
        Gameplay,
    }
}
