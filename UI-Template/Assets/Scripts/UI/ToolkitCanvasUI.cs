using System;
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
        }

        protected virtual void Init()
        {
            
        }
    }
}
