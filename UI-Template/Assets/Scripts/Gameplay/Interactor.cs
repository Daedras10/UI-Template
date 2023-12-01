using System;
using Managers;
using UnityEngine;

namespace Gameplay
{
    public class Interactor : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            DialogsManager.StartDialogEvent?.Invoke();
        }
    }
}