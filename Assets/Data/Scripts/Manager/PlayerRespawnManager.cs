using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnManager : MonoBehaviour
{
    private Transform _respawnPosition;
    internal Transform respawnPosition {set{_respawnPosition = value;} get{return _respawnPosition;}}
    [SerializeField] private Transform _startPosition;
    private static PlayerRespawnManager current;

    void Awake()
    {
        current = this;   
    }
    void Start()
    {
        SetRespawn(_startPosition);
    }

    public static void SetRespawn(Transform pos)
    {
        current.respawnPosition = pos;
    }
    public static Transform GetRespawn()
    {
        return current.respawnPosition;
    }
}
