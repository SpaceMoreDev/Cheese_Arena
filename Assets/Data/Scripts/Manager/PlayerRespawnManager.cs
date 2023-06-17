using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnManager : MonoBehaviour
{
    private Vector3 _respawnPosition;
    internal Vector3 respawnPosition {set{_respawnPosition = value;} get{return _respawnPosition;}}
    Vector3 STARTING_POSITION = new Vector3(12.751121520996094f,-3.019153594970703f,4.111264228820801f);
    private static PlayerRespawnManager current;

    void Awake()
    {
        current = this;   
    }
    void Start()
    {
        SetRespawn(STARTING_POSITION);
    }

    public static void SetRespawn(Vector3 pos)
    {
        current.respawnPosition = pos;
    }
    public static Vector3 GetRespawn()
    {
        return current.respawnPosition;
    }
}
