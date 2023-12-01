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

        private Slider volumeSlider;
        private Toggle muteToggle;
        private MinMaxSlider scaleMinMax;
        private DropdownField colorDropDown;
        private RadioButtonGroup radioButtonGroup;

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
            SettingsChangedCallbacks();
        }

        private void SettingsChangedCallbacks()
        {
            /*
             * VolumeSlider
             * MuteToggle
             * ScaleMinMax
             * ColorDropDown
             * RadioButtonGroup
             */
            
            volumeSlider = root.Q<Slider>("VolumeSlider");
            volumeSlider.RegisterValueChangedCallback(ev => OnVolumeSliderChanged(ev.newValue));
            
            muteToggle = root.Q<Toggle>("MuteToggle");
            muteToggle.RegisterValueChangedCallback(ev => OnMuteToggleChanged(ev.newValue));
            
            scaleMinMax = root.Q<MinMaxSlider>("ScaleMinMax");
            scaleMinMax.RegisterValueChangedCallback(ev => OnScaleMinMaxChanged(ev.newValue));
            
            colorDropDown = root.Q<DropdownField>("ColorDropDown");
            colorDropDown.RegisterValueChangedCallback(ev => OnColorDropDownChanged(ev.newValue));
            
            radioButtonGroup = root.Q<RadioButtonGroup>("RadioButtonGroup");
            radioButtonGroup.RegisterValueChangedCallback(ev => OnRadioButtonGroupChanged(ev.newValue));
            
            
            // Set value to settings by default
            SettingsChanged();
            
            GameManager.OnSettingsChanged += SettingsChanged;
        }
        
        private void SettingsChanged()
        {
            volumeSlider.SetValueWithoutNotify(GameData.volume);
            muteToggle.SetValueWithoutNotify(GameData.soundMuted);
            scaleMinMax.SetValueWithoutNotify(new Vector2(GameData.cubeScaleMin, GameData.cubeScaleMax));

            if (GameData.cubeColor == Color.red)
                colorDropDown.SetValueWithoutNotify("Red");
            else if (GameData.cubeColor == Color.green)
                colorDropDown.SetValueWithoutNotify("Green");
            else if (GameData.cubeColor == Color.blue)
                colorDropDown.SetValueWithoutNotify("Blue");
            else
                colorDropDown.SetValueWithoutNotify("White");
            // TODO : Set radio button
        }
        
        
        private void OnVolumeSliderChanged(float value)
        {
            GameData.volume = value;
            GameManager.instance.ApplySettings();
        }
        
        private void OnMuteToggleChanged(bool value)
        {
            GameData.soundMuted = value;
            GameManager.instance.ApplySettings();
        }
        
        private void OnScaleMinMaxChanged(Vector2 value)
        {
            GameData.cubeScaleMin = value.x;
            GameData.cubeScaleMax = value.y;
            GameManager.instance.ApplySettings();
        }
        
        private void OnColorDropDownChanged(string value)
        {
            switch (value)
            {
                case "Red":
                    GameData.cubeColor = Color.red;
                    break;
                case "Green":
                    GameData.cubeColor = Color.green;
                    break;
                case "Blue":
                    GameData.cubeColor = Color.blue;
                    break;
                default:
                    GameData.cubeColor = Color.white;
                    break;
            }
            GameManager.instance.ApplySettings();
        }
        
        private void OnRadioButtonGroupChanged(int value)
        {
            // TODO : Change radio button
            GameManager.instance.ApplySettings();
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
