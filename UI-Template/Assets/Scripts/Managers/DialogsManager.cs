using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UI;
using UnityEngine;

namespace Managers
{
    public class DialogsManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private DialogUI dialogUI;

        [Header("Settings")]
        [SerializeField] private DialogsSO dialogsSo;
        [SerializeField, Tooltip("In seconds")] private float timeBetweenLetters = 0.1f;
        [SerializeField, Tooltip("In seconds")] private float timeBetweenDialogs = 1.5f;
        
        [SerializeField] private bool dialogIsDisplaying;
        [SerializeField] private List<DialogsData> dialogsToDisplay = new List<DialogsData>();
        
        [SerializeField] private bool nextIsChoice;
        [SerializeField] private List<string> choicesToDisplay = new List<string>();

        public static Action StartDialogEvent;
        
        private void Start()
        {
            dialogsToDisplay = dialogsSo.Dialogs.ToList();
            StartDialogEvent += StartDialog;
        }

        [ContextMenu("Start Dialog")]
        public void StartDialog()
        {
            StartDialogEvent -= StartDialog;
            dialogUI.OnDialogFullyShown += ContinueDialogIfPossible;
            DialogUI.TryToSkip += TryToSkip;
            dialogUI.overrideTimeAndFinish = false;
            ContinueDialogIfPossible();
        }
        
        private IEnumerator FinishDialog()
        {
            yield return new WaitForSeconds(timeBetweenDialogs*2);
            
            dialogUI.OnDialogFullyShown -= ContinueDialogIfPossible;
            DialogUI.TryToSkip -= TryToSkip;
            dialogUI.Hide();
        }
        
        private void ContinueDialogAfterChoice(string choice)
        {
            DialogUI.OnChoiceSelected -= ContinueDialogAfterChoice;
            
            dialogsToDisplay.Insert(0, new DialogsData(choice, new List<string>()));
            ContinueDialogIfPossible();
        }
        
        private void ContinueDialogIfPossible()
        {
            dialogIsDisplaying = false;
            
            if (nextIsChoice)
            {
                nextIsChoice = false;
                
                List<(string, string)> choices = choicesToDisplay.Select(t => (JsonLoader.GetChoosenChoice(t), JsonLoader.GetChoice(t).nextdialog)).ToList();

                DialogUI.OnChoiceSelected += ContinueDialogAfterChoice;
                dialogUI.DisplayChoices(choices);
                return;
            }
            
            
            if (dialogsToDisplay.Count == 0)
            {
                StartCoroutine(FinishDialog() );
                return;
            }
            
            if (dialogsToDisplay[0].ChoiceAfter)
            {
                nextIsChoice = true;
                choicesToDisplay = dialogsToDisplay[0].choicesID.ToList();
            }
            
            StartCoroutine(ContinueDialog());
        }

        private IEnumerator ContinueDialog()
        {
            yield return new WaitForSeconds(timeBetweenDialogs);
            
            dialogIsDisplaying = true;
            DialogsData dialogData = dialogsToDisplay[0];
            dialogsToDisplay.RemoveAt(0);
            
            var dialog = JsonLoader.GetDialog(dialogData.id);
            
            DisplayDialog(dialog, GameData.chosenLanguage);
        }

        private void DisplayDialog(JsonLoader.Dialog dialog, ChosenLanguage chosenLanguage)
        {
            switch (chosenLanguage)
            {
                case ChosenLanguage.French:
                    dialogUI.DisplayDialog(dialog.textfr, dialog.talkerfr, dialog.sideleft == 1, timeBetweenLetters);
                    break;
                case ChosenLanguage.English:
                    dialogUI.DisplayDialog(dialog.texten, dialog.talkeren, dialog.sideleft == 1, timeBetweenLetters);
                    break;
                default:
                    dialogUI.DisplayDialog(dialog.textfr, dialog.talkerfr, dialog.sideleft == 1, timeBetweenLetters);
                    break;
            }
        }

        private void TryToSkip()
        {
            if (dialogIsDisplaying)
            {
                dialogUI.overrideTimeAndFinish = true;
            }
            
            // cancel wait if possible
        }
    }
}