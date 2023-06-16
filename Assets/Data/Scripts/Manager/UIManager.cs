using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Managers{
    
    public class UIManager : MonoBehaviour
    {
        [SerializeField] public Canvas PlayerCanvas;
        public static bool busyUI = false;
        public static UIManager current;

        void Awake()
        {
            current = this;

            DialogueManager.startDialogueAction += this.dialogueStart;
            DialogueManager.EndDialogueAction += this.dialogueEnd;

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
            DialogueManager.current.dialogueUI.SetActive(true);
            InputManager.ToggleActionMap(InputManager.inputActions.UI);
            CM_CamerasSetup.FocusMouse(false);
            // CM_CamerasSetup.PauseCamera(true);
        }

        private void dialogueEnd()
        {
            DialogueManager.current.dialogueUI.SetActive(false);
            InputManager.ToggleActionMap(InputManager.inputActions.General);
            CM_CamerasSetup.FocusMouse(true);
            // CM_CamerasSetup.PauseCamera(false);
        }
    }

}