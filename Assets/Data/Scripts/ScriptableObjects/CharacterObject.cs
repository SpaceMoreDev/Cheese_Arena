using System.IO;
using UnityEngine;
using Managers;

public enum Faction
{
    Friend,
    Foe
}

[CreateAssetMenu(fileName = "New Character", menuName = "Characters/Create Character")]
public class CharacterObject : ScriptableObject
{
    public int ID;
    public string CharacterName;
    public AbilityObject abilityObject; 
    public Faction faction; 

    [Header("Expressions")]
    public Sprite normal;
    public Sprite surprise;
    public Sprite happy;
    public Sprite sad;
    public Sprite cry;
    public Sprite angry;


    [Header("Files")]
    public GameObject prefap;
    public TextAsset DialoguesFile;
}