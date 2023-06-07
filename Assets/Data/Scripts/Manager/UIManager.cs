using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Managers{
    
    public class UIManager : MonoBehaviour
    {
        [SerializeField] public GameObject inventoryMenu;
        [SerializeField] public GameObject inventoryRow;
        [SerializeField] public GameObject characterMenu;
        public static bool busyUI = false;

        private bool isHidden = false;
        Transform inventoryContainer;
        Dictionary<string,int> inventory;
        public static UIManager current;

        void Awake()
        {
            current = this;

            DialogueManager.startDialogueAction += this.dialogueStart;
            DialogueManager.EndDialogueAction += this.dialogueEnd;
            inventoryContainer = inventoryMenu.transform.Find("ScrollArea").GetChild(0).transform;

        }

        void OnDisable()
        {
            DialogueManager.startDialogueAction -= this.dialogueStart;
            DialogueManager.EndDialogueAction -= this.dialogueEnd;
        }

        public static IEnumerator FadeOut(Graphic Component, float fade)
        {
            Color startColor = Component.color;
            Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0.0f);

            float elapsedTime = 0.0f;
            while (elapsedTime < fade)
            {
                float t = elapsedTime / fade;
                Component.color = Color.Lerp(startColor, endColor, t);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            Component.color = endColor;
        }


        private void dialogueStart()
        {
            busyUI = true;
            characterMenu.SetActive(false);
            DialogueManager.current.dialogueUI.SetActive(true);
            InputManager.ToggleActionMap(InputManager.inputActions.UI);
            CM_CamerasSetup.FocusMouse(false);

        }


        private void dialogueEnd()
        {
            DialogueManager.current.dialogueUI.SetActive(false);
            InputManager.ToggleActionMap(InputManager.inputActions.General);
            CM_CamerasSetup.FocusMouse(true);
        }
    }

}