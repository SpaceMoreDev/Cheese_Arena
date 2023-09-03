using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Managers;

public class ChoicesButtons : MonoBehaviour
{
    [HideInInspector] public Choice choice;

    void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = choice.text;
        GetComponent<Button>().onClick.AddListener(onPressed);
    }
    void onPressed()
    {
       DialogueManager.SelectedChoice(choice);
       DialogueManager.inChoices = false;
    }
}
