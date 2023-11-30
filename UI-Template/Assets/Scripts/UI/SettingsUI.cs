using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class SettingsUI : ToolkitCanvasUI
    {
        public event Action OnClose;

        private VisualElement closeBg;


        protected override void Init()
        {
            root.style.visibility = Visibility.Hidden;
            
            // Close Settings on callback
            closeBg = root.Q<VisualElement>("CloseBg");
            closeBg.RegisterCallback<ClickEvent>(ev =>HideSettings());
            
            // Block propagation
            var settingContainer = root.Q<VisualElement>("SettingContainer");
            settingContainer.RegisterCallback<ClickEvent>(ev => ev.StopPropagation());
        }

        private void HideSettings()
        {
            root.style.visibility = Visibility.Hidden;
            OnClose?.Invoke();
        }
        
        public void ShowSettings()
        {
            root.style.visibility = Visibility.Visible;
        }
        
        public bool IsVisible()
        {
            return root.style.visibility == Visibility.Visible;
        }
    }
}
