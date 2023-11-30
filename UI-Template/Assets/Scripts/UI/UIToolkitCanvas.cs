using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class UIToolkitCanvas : MonoBehaviour
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
