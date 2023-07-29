using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CharacterStats
{
    int _health;
    GameObject _spawnObject;
}

public class Character : MonoBehaviour
{
    [SerializeField] public int health = 100;
    [SerializeField] public int damage = 10;

    public void Awake()
    {
        
    }
    void OnDestroy()
    {
        
    }
}
