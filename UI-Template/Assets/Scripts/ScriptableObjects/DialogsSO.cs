using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "new Dialog", menuName = "ScriptableObjects/DialogsSO", order = 0)]
    public class DialogsSO : ScriptableObject
    {
        [field:Header("---DialogsSO---")]
        [field: SerializeField] public List<DialogsData> Dialogs { get; private set; }
    }

    [Serializable]
    public class DialogsData
    {
        [field: SerializeField] public string id;
        [field: SerializeField] public bool ChoiceAfter => choicesID.Count > 0;
        [field: SerializeField] public List<string> choicesID;

        public DialogsData(string id, List<string> choicesID)
        {
            this.id = id;
            this.choicesID = new List<string>(choicesID);
        }
    }
}