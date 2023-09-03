using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUp : MonoBehaviour, I_ActivateActions
{
    [SerializeField] private GameObject selectionUI;
    public GameObject DisplayUI {get{return selectionUI;}}

    private bool consumed = false;
    public bool Activated {get{return consumed;}}

    public void Activate(Character character)
    {
        character.Skills.IncreaseSkill(SKILL_TYPE.STRENGTH);
        Debug.Log($"{character.characterStats.Name} has strength: {character.Skills.Strength}");
    }
}
