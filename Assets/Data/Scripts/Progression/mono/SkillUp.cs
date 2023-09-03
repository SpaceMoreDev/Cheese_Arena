using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class SkillUp : MonoBehaviour, I_ActivateActions
{
    [SerializeField] private GameObject selectionUI;
    public GameObject DisplayUI {get{return selectionUI;}}

    private bool consumed = false;
    public bool Activated {get{return consumed;}}

    [SerializeField] private  GameObject _upgradeMenu;

    public void Activate(Character character)
    {
        // character.Skills.IncreaseSkill(SKILL_TYPE.STRENGTH);
        // Debug.Log($"{character.characterStats.Name} has strength: {character.Skills.Strength}");
        consumed = !consumed;
        _upgradeMenu.SetActive(consumed);
        Skillbutton.character = character;
        if(consumed == true)
        {
            Cursor.lockState = CursorLockMode.None;
            InputManager.ToggleActionMap(InputManager.inputActions.UI);
            PlayerCameraHandler.Instance.SetCamerPOV(false);
        }else{
            InputManager.ToggleActionMap(InputManager.inputActions.General);
            Cursor.lockState = CursorLockMode.Locked;
            PlayerCameraHandler.Instance.SetCamerPOV(true);

        }
    }
}
