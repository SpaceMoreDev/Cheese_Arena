/*
    Here I will put functions the inputs being pressed.
    Will allow me to assign the same input to different scripts/gameObjects.
*/
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using Managers;

public class PlayerInputHandler : MonoBehaviour
{
    #region Input Actions
    public static InputAction Movement;
    public static InputAction Jump;
    public static InputAction Interact;
    #endregion


    #region Private Variables
    public static PlayerInputHandler current;
    #endregion

    void Awake()
    {
        current = this;
        Movement = InputManager.inputActions.General.Move;
        Jump = InputManager.inputActions.General.Jump; 
        Interact = InputManager.inputActions.General.Interact;
    }


    void Start()
    {
        InputManager.ToggleActionMap(InputManager.inputActions.General);

        #region General ActionMap
            InputManager.inputActions.General.Escape.started += this.Escape;
            // InputManager.inputActions.General.MousePosition.performed += this.MousePositionMove;
        #endregion

        #region UI ActionMap
            InputManager.inputActions.UI.Escape.started += this.Escape;
            InputManager.inputActions.UI.Accept.started += this.Accept;
        #endregion
    }

    void OnDisable()
    {
        #region General ActionMap
            InputManager.inputActions.General.Escape.started -= this.Escape;
            // InputManager.inputActions.General.MousePosition.performed -= this.MousePositionMove;
        #endregion

        #region UI ActionMap
            InputManager.inputActions.UI.Escape.started -= this.Escape;
            InputManager.inputActions.UI.Accept.started -= this.Accept;
        #endregion
    }

    #region InputFunctions

    #endregion

    void Escape(InputAction.CallbackContext context)
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
    
    void Accept(InputAction.CallbackContext context)
    {
        if(!DialogueManager.inChoices)
        {
            // skip to next cutscene
            DialogueManager.current.Next();
        }
    }
}
