using System.IO;
using UnityEngine;
using Managers;


public enum AbilityType
{
    Telekinisis,
    Fire,
    Snake
}

[CreateAssetMenu(fileName = "New Ability", menuName = "Create Ability")]
public class AbilityObject : ScriptableObject
{
    public int abilityID;
    public string abilityName;
    public Sprite icon;
    public AbilityType type;
    public string abilityDescription;
}