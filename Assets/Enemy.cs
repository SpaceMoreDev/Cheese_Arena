using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Health healthbar;
    [SerializeField] public Transform arrowSpawn;

    public GameObject arrowPrefab;
    public string playerTag = "Player";

    private GameObject player;
    private float timer;

    bool shooting = false;

    private void Start()
    {
        player = EnemyManager.current.player;
        // if(player != null){Debug.Log($"found player! {player.name}");}else{Debug.Log($"player not found!");}
        healthbar.character = gameObject;
        timer = EnemyManager.current.shootInterval;
    }

    private void Update()
    {
        
       timer -= Time.deltaTime;

        if (timer <= 0f && player != null)
        {
            // Check if the player is in sight
            Vector3 direction = player.transform.position - gameObject.transform.position;

            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            // Perform the raycast
            if (Physics.Raycast(ray, out hit, EnemyManager.current.maxDistance, EnemyManager.current.visionLayer))
            {
                // Check if the hit object has the player tag
                if (hit.collider.CompareTag(playerTag))
                {
                    // Player is in sight
                    Debug.Log("Player is in sight!");
                    shooting = true;
                }
                else
                {
                    // Log the tag of the hit object for debugging
                    Debug.Log("Hit object tag: " + hit.collider.name);
                    shooting = false;
                }
               
            }
            else
            {
                shooting = false;
            }
            
            if(shooting)
            {
                Shoot(direction);
                
            }

             Debug.DrawLine(ray.origin, ray.origin + ray.direction * 100f, Color.blue);
        }
    }

    void Shoot(Vector3 direction)
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawn.position , Quaternion.LookRotation(direction));
        // Reset the timer
        timer = EnemyManager.current.shootInterval;
    }
}
