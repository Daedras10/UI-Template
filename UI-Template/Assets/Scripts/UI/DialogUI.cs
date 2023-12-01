using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogUI : ToolkitCanvasUI
{
    private VisualElement dialogParent;
    private Label leftCharacterName;
    private Label rightCharacterName;
    
    private VisualElement leftCharacter;
    private VisualElement rightCharacter;
    
    private VisualElement leftSide;
    private VisualElement rightSide;

    private VisualElement choicesVisualElement;
    
    public bool overrideTimeAndFinish = false;

    public event Action OnDialogFullyShown; 
    public static event Action<string> OnChoiceSelected;
    
    protected override void Init()
    {
        dialogParent = root.Q<VisualElement>("DialogsScroll");
        leftCharacterName = root.Q<Label>("LeftCharacterName");
        rightCharacterName = root.Q<Label>("RightCharacterName");
        
        leftCharacter = root.Q<VisualElement>("LeftImage");
        rightCharacter = root.Q<VisualElement>("RightImage");
        
        leftSide = root.Q<VisualElement>("Left");
        rightSide = root.Q<VisualElement>("Right");
        
        choicesVisualElement = root.Q<VisualElement>("Choices");
        
        ClearChoices();
        Hide();
    }
    
    private void HideLeftSide()
    {
        leftSide.style.visibility = Visibility.Hidden;
    }
    
    private void HideRightSide()
    {
        rightSide.style.visibility = Visibility.Hidden;
    }
    
    private void ShowLeftSide()
    {
        leftSide.style.visibility = Visibility.Visible;
    }
    
    private void ShowRightSide()
    {
        rightSide.style.visibility = Visibility.Visible;
    }
    
    public void DisplayDialog(string dialogText, string speakerName, bool leftSide = true, float timeBetweenLetters = 0.1f, bool instant = false)
    {
        if (root.style.visibility == Visibility.Hidden) Show();


        if (leftSide)
        {
            leftCharacterName.text = speakerName;
            ShowLeftSide();
        }
        else
        {
            rightCharacterName.text = speakerName;
            ShowRightSide();
        }
        
        // Images would be here if I had any (and read according to speakerName id)
        
        if (instant)
        {
            AddDialogInstant(dialogText, speakerName);
            return;
        }

        StartCoroutine(ShowDialogSlowly(dialogText, speakerName, timeBetweenLetters));
    }

    private IEnumerator ShowDialogSlowly(string dialogText, string speakerName, float timeBetweenLetters)
    {
        string str = "<b>" + speakerName + "</b> : ";
        var dialog = new Label(str);
        dialog.AddToClassList("DialogText");
        dialogParent.Add(dialog);

        foreach (var letter in dialogText)
        {
            dialog.text += letter;
            if (!overrideTimeAndFinish) yield return new WaitForSeconds(timeBetweenLetters);
        }
        
        OnDialogFullyShown?.Invoke();
    }
    
    private void AddDialogInstant(string dialogText, string speakerName)
    {
        string str = "<b>" + speakerName + "</b> : " + dialogText;
        var dialog = new Label(str);
        dialog.AddToClassList("DialogText");
        
        dialogParent.Add(dialog);
        OnDialogFullyShown?.Invoke();
    }

    private void ClearChoices()
    {
        choicesVisualElement.Clear();
    }
    
    public void DisplayChoices(List<(string, string)> choices)
    {
        ClearChoices();

        for (int i = 0; i < choices.Count; i++)
        {
            AddChoice(choices[i].Item1, choices[i].Item2, i + 1);
        }
    }
    
    private void AddChoice(string choiceText, string target, int index)
    {
        Label label = new Label("" + index + ". " + choiceText);
        label.AddToClassList("Choice");
        label.AddToClassList("DialogText");
        label.RegisterCallback<ClickEvent,string>( SelectChoice, target );
        
        choicesVisualElement.Add(label);
    }

    private void SelectChoice(ClickEvent ce, string choice)
    {
        OnChoiceSelected?.Invoke(choice);
        ClearChoices();
    }

    public void Hide()
    {
        root.style.visibility = Visibility.Hidden;
        HideLeftSide();
        HideRightSide();
    }
    
    private void Show()
    {
        root.style.visibility = Visibility.Visible;
    }
}
