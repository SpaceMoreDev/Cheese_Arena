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
    public string CharacterName;
    public Faction faction; 

    [Header("Expressions")]
    public Sprite normal;
    public Sprite surprise;
    public Sprite happy;
    public Sprite sad;
    public Sprite cry;
    public Sprite angry;


    [Header("Files")]
    public TextAsset DialoguesFile;
}