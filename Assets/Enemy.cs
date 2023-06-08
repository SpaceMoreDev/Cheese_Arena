using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Health healthbar;
    [SerializeField] public Transform arrowSpawn;

    public GameObject arrowPrefab;
    public string playerTag = "Player";
    public float shootInterval = 1f;

    public static GameObject player;
    private float timer;

    bool shooting = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
        // if(player != null){Debug.Log($"found player! {player.name}");}else{Debug.Log($"player not found!");}
        healthbar.character = gameObject;
        timer = shootInterval;
    }

    private void Update()
    {
       timer -= Time.deltaTime;

        if (timer <= 0f && player != null)
        {
            // Check if the player is in sight
            Vector3 direction = player.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {

                shooting = true;
            }
            
            if(shooting)
            {
                Shoot(direction);
                
            }
        }
    }

    void Shoot(Vector3 direction)
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawn.position , Quaternion.LookRotation(direction));
        // Reset the timer
        timer = shootInterval;
    }
}
