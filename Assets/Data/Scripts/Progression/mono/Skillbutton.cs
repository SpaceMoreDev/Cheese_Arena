using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skillbutton : MonoBehaviour
{
    [SerializeField] private SKILL_TYPE skillType; 
    [SerializeField] private Button _plusButton;
    [SerializeField] private Button _minusButton;
    [SerializeField] private Transform pointsPanel;
    internal static Character character;
   private void Start() {
        _plusButton.onClick.AddListener(AddPoint);
        _minusButton.onClick.AddListener(RemovePoint);

        foreach(Transform i in pointsPanel)
        {
            Image image = i.GetComponent<Image>();
            image.color = Color.gray;
        }
        UpdatePoints();
   }

   void AddPoint()
   {    
        character.Skills.IncreaseSkill(skillType);
        UpdatePoints();
   }
   void RemovePoint()
   {
        character.Skills.DecreaseSkill(skillType);
        UpdatePoints();

   }

   void UpdatePoints()
   {    
        int skillCount = 0;
        int ct = 0;
        skillCount = character.Skills.GetSkillNum(skillType);
        foreach(Transform i in pointsPanel)
        {
            Image image = i.GetComponent<Image>();
            if(ct<skillCount){    
                image.color = Color.white;
                ct++;
            }else{
                image.color = Color.gray;
            }
        } 

        if(character.Skills.GetSkillNum(skillType) >= pointsPanel.childCount)
        {
            _plusButton.interactable = false;
        }
        else{
            _plusButton.interactable = true;
        }
        if(character.Skills.GetSkillNum(skillType) <= 0)
        {
            _minusButton.interactable = false;
        }else{
            _minusButton.interactable = true;
        }

        Debug.Log("-------------------------------------");
        Debug.Log($"strength: {character.Skills.Strength}");
        Debug.Log($"speed: {character.Skills.Speed}");
        Debug.Log($"intellect: {character.Skills.Intellect}");
   }
}
