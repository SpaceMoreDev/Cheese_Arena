using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Managers;
using TMPro;

public class DialogueChoices : MonoBehaviour
{

    [SerializeField] Transform choicesPanel;
    [SerializeField] GameObject choicePrefap;
    public static event Action<Choice> choiceAction;
    public Choice[] choices;

    void Awake()
    {
        foreach(Choice choice in choices)
        {
            GameObject obj = Instantiate(choicePrefap, transform.GetChild(0));
            ChoicesButtons objScript = obj.GetComponent<ChoicesButtons>();
            objScript.choice = choice;
        }
    }

    public static Choice StartChoiceAction()
    {
        if(choiceAction != null)
        {
            choiceAction(new Choice());
        }
        
        return new Choice();
    }
}
