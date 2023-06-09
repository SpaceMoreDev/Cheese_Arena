
using UnityEngine.AI;
using UnityEngine;
using Managers;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Health healthbar;
    [SerializeField] public Transform arrowSpawn;
    [SerializeField] public Animator animator;

    public GameObject arrowPrefab;
    public NavMeshAgent navMesh;
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
        navMesh = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Vector3 direction = player.transform.position - gameObject.transform.position;
        timer -= Time.deltaTime;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
        if( distance > EnemyManager.current.maxDistance)
        {
            navMesh.isStopped = false;
            navMesh.SetDestination(player.transform.position);
        }
        else if(distance<EnemyManager.current.minDistance){
            //todo when player is close make enemy hit with melee
            // animator.Play("Attack");
        }
        else{
            navMesh.isStopped = true;
            if (timer <= 0f && player != null)
            {
                // Check if the player is in sight
                Ray ray = new Ray(transform.position, direction);
                RaycastHit hit;

                // Perform the raycast
                if (Physics.Raycast(ray, out hit, EnemyManager.current.maxDistance, EnemyManager.current.visionLayer))
                {
                    // Check if the hit object has the player tag
                    if (hit.collider.CompareTag(playerTag))
                    {
                        // Player is in sight
                        shooting = true;
                    }
                    else
                    {
                        // Log the tag of the hit object for debugging
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
    }

    void Shoot(Vector3 direction)
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawn.position , Quaternion.LookRotation(direction));
        // Reset the timer
        timer = EnemyManager.current.shootInterval;
    }
}
