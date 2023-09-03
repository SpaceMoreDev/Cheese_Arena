using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CharacterStats
{
    public string Name;
    public float Health;
    public float Stamina;
   

    public CharacterStats(string name = "John Doe", 
    float health = 100f, 
    float stamina = 100f)
    {
        Name = name;
        Health = health;
        Stamina = stamina;
    }
};

public class Character : MonoBehaviour
{
    [SerializeField] internal CharacterStats characterStats;
    [SerializeField] internal Skills Skills;

    private void Awake() {
        Skills = new Skills();
        Debug.Log($"{characterStats.Name} has strength: {Skills.Strength}");
        Debug.Log($"{characterStats.Name} has speed: {Skills.Speed}");
        Debug.Log($"{characterStats.Name} has intellect: {Skills.Intellect}");
    }
}
