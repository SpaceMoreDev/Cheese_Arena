using System.Linq.Expressions;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Managers
{

    [System.Serializable]
    public enum eventsActions
    {
        NoEvent,
        BecomePlayer, //todo add character to player list.
        RemovePlayer, //todo add character to player list.
        StartChoices,
        Follow, //todo make character follow player.
        Wait, //todo make character wait in place.
        RemoveActor,
        ChangeScene
    };

    [System.Serializable]
    public enum CharacterExpressions
    {
        normal,
        surprise,
        happy,
        sad,
        cry,
        angry,
    };

    [System.Serializable]
    public class Choice
    {
        public string text;
        public int dialogueID;
        public int lineID;
        public eventsActions choice_event;
    }
    
    [System.Serializable]
    public class DialogueLines
    {

        public eventsActions line_event;
        public int line_id;
        public string name;
        public CharacterExpressions expression;
        public string text;
        public Choice[] choices;
        public static event Action<eventsActions> dialogueLineAction;

        public void getChoices()
        {
            if(choices.Length>0)
            {
                var choicePanel = DialogueManager.current.ChoiceDialoguePrefap;
                choicePanel.GetComponent<DialogueChoices>().choices = choices;
                
                DialogueManager.current.choiceUI = GameObject.Instantiate(choicePanel,DialogueManager.current.transform.parent);    
            }
        }
    }

    [System.Serializable]
    public class DialogueData
    {
        public string scene;
        public DialogueLines [] lines;
    }

    [System.Serializable]
    public class DialoguesList
    {
        public DialogueData [] dialogues;
    }


    public class DialogueManager : MonoBehaviour
    {
        [SerializeField]  TMP_Text dialogueText;
        [SerializeField]  TMP_Text nameCharacter;
        [SerializeField]  GameObject diaEnd;
        [SerializeField]  float delayTime = 0.1f;
        [SerializeField]  GameObject characterPrefap;
        [SerializeField]  GameObject LeftCharacter;
        [SerializeField]  GameObject RightCharacter;
        [SerializeField] internal GameObject ChoiceDialoguePrefap;

        
        public static bool inDialogue = false;
        // public static Choice selectedChoice;
        public static bool inChoices = false;
        public static event Action startDialogueAction;
        public static event Action EndDialogueAction;
        public static DialogueManager current;
        public GameObject dialogueUI;

        public DialogueLines currentLine;

        internal GameObject choiceUI;


        private DialoguesList jsonData; // stores the parsed JSON data
        private int currentSceneIndex = 0; // index of the current scene in the JSON data
        private int currentLineIndex = 0; // index of the current line in the current scene
        private CharacterObject character;
        public GameObject characterGameObject;
        private List<KeyValuePair<CharacterObject, GameObject>> dialogueCharacters;
        
        public void StartDialogueAction(eventsActions line_event, GameObject characterObj, Choice? choice)
        {
            if(line_event == eventsActions.StartChoices)
            {
                NotificationManager.StartNotification("Started choices event..");
                DialogueManager.inChoices = true;
                currentLine.getChoices();
            }
            else if(line_event == eventsActions.ChangeScene)
            {
                NotificationManager.StartNotification("Started change scene event..");
            }
            else if(line_event == eventsActions.Follow)
            {
                NotificationManager.StartNotification("Following the player..");
            }
            else if(line_event == eventsActions.Wait)
            {
                NotificationManager.StartNotification("Waiting..");
            }
            else if(line_event == eventsActions.RemoveActor)
            {
                NotificationManager.StartNotification("Started remove actor event..");
            }
        }

        void Awake()
        {
            current = this;
            dialogueCharacters = new List<KeyValuePair<CharacterObject, GameObject>>();
            dialogueUI =transform.GetChild(0).gameObject;
            dialogueUI.SetActive(false);
        }

        public static void SelectedChoice(Choice choiceSelected)
        {
            // selectedChoice = choiceSelected;
            current.currentLineIndex = choiceSelected.lineID;
            current.currentSceneIndex = choiceSelected.dialogueID;

            current.StartDialogueAction(choiceSelected.choice_event,current.characterGameObject,choiceSelected);
            
            Destroy(DialogueManager.current.choiceUI);
            DialogueManager.current.DisplayNextLine();
        }

        public static void StartDialogue (GameObject startingObject, CharacterObject character, int diaID)
        {
            inDialogue = true;
            current.character = character;
            current.characterGameObject = startingObject;
            current.currentSceneIndex = diaID;
            
            if(startDialogueAction != null)
            {
                startDialogueAction();
            }

            current.GetDialogue(character.DialoguesFile);    
        }

        private void GetDialogue(TextAsset dialogue)
        {
            jsonData = JsonUtility.FromJson<DialoguesList>(dialogue.text);
            currentLineIndex = 0;

            DisplayNextLine();
        }

        private void CharacterLineSetup(string characterToSetup,CharacterExpressions expression , bool isSpwanedRight, CharacterObject othercharacter)
        {   
            Transform SpawnLocation;
            bool direction;
            if(isSpwanedRight)
            { 
                SpawnLocation = RightCharacter.transform; 
                direction = false;
            }
            else
            { 
                SpawnLocation = LeftCharacter.transform; 
                direction = true;
            }

            if(othercharacter != null)
            {
                if(!CheckCharacterDialogue(othercharacter))
                {
                    // print("doesnt contain character");
                    AddCharactersToDialogue(othercharacter,SpawnLocation,direction);
                }
                DialogueActivateCharacter(othercharacter);

                GameObject characterGameobject = dialogueCharacters.Find(pair => pair.Key ==othercharacter).Value;
                ChangeExpression(othercharacter,characterGameobject,expression);
                nameCharacter.text = othercharacter.CharacterName;
            }
            else
            {
                nameCharacter.text = characterToSetup;
            }
        }

        private void AddCharactersToDialogue(CharacterObject characterToAdd ,Transform spawnSide , bool flip)
        {   
            var characterSprite =Instantiate(characterPrefap, spawnSide.transform);
                
            if(flip)
            {
                characterSprite.transform.localScale *= new Vector2(-1,1);
            }
            dialogueCharacters.Add(new KeyValuePair<CharacterObject, GameObject>(characterToAdd,characterSprite));
        }

        private void InsertToTop(CharacterObject characterToInsert)
        {
            KeyValuePair<CharacterObject, GameObject> tempobj = dialogueCharacters.Find(pair => pair.Key == characterToInsert);

            dialogueCharacters.Remove(dialogueCharacters.Find(pair => pair.Key == characterToInsert));
            dialogueCharacters.Insert(0,tempobj);
        }

        private void ClearAllCharacters()
        {
            dialogueCharacters.Clear();
            int childCount = LeftCharacter.transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                Transform childTransform = LeftCharacter.transform.GetChild(i);
                GameObject.DestroyImmediate(childTransform.gameObject);
            }
            
            childCount = RightCharacter.transform.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                Transform childTransform = RightCharacter.transform.GetChild(i);
                GameObject.DestroyImmediate(childTransform.gameObject);
            }
        }

        void DialogueActivateCharacter(CharacterObject characterToActivate)
        {
            foreach(var child in dialogueCharacters)
            {
                child.Value.GetComponentInChildren<Graphic>().color = Color.gray;
            }

            if(dialogueCharacters.Exists(pair => pair.Key == characterToActivate))
            {
                dialogueCharacters.Find(pair => pair.Key ==characterToActivate).Value.GetComponentInChildren<Graphic>().color = Color.white;
                InsertToTop(characterToActivate);
            }
            int order = dialogueCharacters.Count-1;
            foreach(var child in dialogueCharacters)
            {
                child.Value.GetComponentInChildren<Transform>().GetComponent<Canvas>().sortingOrder = order;
                // print($"\n{child.Key} - {order}");
                order--;
            }
        }

        bool CheckCharacterDialogue(CharacterObject charobj)
        {
            if(dialogueCharacters.Exists(pair => pair.Key ==charobj))
            {
                return true;
            }
            return false;
        }

        private void DisplayNextLine()
        {
            diaEnd.SetActive(false);
            // check if we've reached the end of the current scene
            if (currentLineIndex >= jsonData.dialogues[currentSceneIndex].lines.Length)
            {
                inDialogue = false;
                if (EndDialogueAction != null)
                {
                    ClearAllCharacters();
                    EndDialogueAction();
                }
            }
            else
            {
                // display the next line of dialogue
                currentLine = jsonData.dialogues[currentSceneIndex].lines[currentLineIndex];
                StartDialogueAction(currentLine.line_event,characterGameObject,null);
                
                string fullText = currentLine.text;
                
                CharacterObject othercharacter;
                switch(currentLine.name)
                {
                    case "#char#":
                        othercharacter= CharacterManager.FindCharacter(character.CharacterName);
                        CharacterLineSetup(character.CharacterName,currentLine.expression,true, othercharacter);
                        break;

                    case "#player#":
                        CharacterLineSetup(TP_PlayerController.current.playerCharacter.CharacterName,currentLine.expression,false,TP_PlayerController.current.playerCharacter);
                        break;

                    default:
                        othercharacter= CharacterManager.FindCharacter(currentLine.name);
                        if(othercharacter.faction == Faction.Foe)
                        {
                            CharacterLineSetup(currentLine.name,currentLine.expression,true,othercharacter); 
                        }
                        else
                        {
                            CharacterLineSetup(currentLine.name,currentLine.expression,false,othercharacter); 
                        }         
                        break;
                }
                
                StartCoroutine(DisplayDialogue(fullText));
                currentLineIndex++;
            }
        }

        public void ChangeExpression(CharacterObject charobj,GameObject character, CharacterExpressions expression)
        {
            switch(expression)
            {
                case CharacterExpressions.normal:
                    character.transform.GetChild(0).GetComponent<Image>().sprite = charobj.normal;
                    break;
                case CharacterExpressions.surprise:
                    character.transform.GetChild(0).GetComponent<Image>().sprite = charobj.surprise;
                    break;
                case CharacterExpressions.happy:
                    character.transform.GetChild(0).GetComponent<Image>().sprite = charobj.happy;
                    break;
                case CharacterExpressions.sad:
                    character.transform.GetChild(0).GetComponent<Image>().sprite = charobj.sad;
                    break;
                case CharacterExpressions.cry:
                    character.transform.GetChild(0).GetComponent<Image>().sprite = charobj.cry;
                    break;
                case CharacterExpressions.angry:
                    character.transform.GetChild(0).GetComponent<Image>().sprite = charobj.angry;
                    break;
            }
        }

        private IEnumerator DisplayDialogue(string fullText)
        {
            string currentText = "";
            for (int i = 0; i <= fullText.Length; i++)
            {
                currentText = fullText.Substring(0, i);
                dialogueText.text = currentText;
                yield return new WaitForSeconds(delayTime);
            }
            diaEnd.SetActive(true);
        }

        public void Next()
        {
            StopAllCoroutines();
            DisplayNextLine();
        }
    }
}
