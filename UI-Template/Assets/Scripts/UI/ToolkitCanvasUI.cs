using System;
using Managers;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class ToolkitCanvasUI : MonoBehaviour
    {
        [SerializeField] protected UIDocument menuUiDocument;
        protected VisualElement root;

        void Start()
        {
            root = menuUiDocument.rootVisualElement;
            Init();
            
            UpdateLanguage();
            GameManager.OnLanguageChanged += UpdateLanguage;
        }

        protected virtual void Init()
        {
            
        }
        
        protected virtual void UpdateLanguage()
        {
            
        }
    }
}
