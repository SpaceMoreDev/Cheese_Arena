
using UnityEngine.AI;
using UnityEngine;
using Managers;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Health healthbar;
    [SerializeField] public Transform arrowSpawn;
    [SerializeField] GameObject spawnPickUp;
    [SerializeField] public Animator animator;
    [SerializeField] private fireTrigger fireTrigger;
    [SerializeField] EnemyHit hitscan;
    public GameObject arrowPrefab;
    public NavMeshAgent navMesh;
    private GameObject player;

    bool shooting = false;
    bool attacking = false;
    public bool alive = true;
    float slideTimer = 0;
    Vector3 direction;

    private void Start()
    {
        player = EnemyManager.current.player;
        animator = GetComponent<Animator>();
        // if(player != null){Debug.Log($"found player! {player.name}");}else{Debug.Log($"player not found!");}
        healthbar.character = gameObject;
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.isStopped = true;

        Health.healthBarEmpty += this.Death;
    }

    void OnDestroy()
    {
        Health.healthBarEmpty -= this.Death;
    }

    void Death(GameObject ctx)
    {
        if(ctx == this.gameObject)
        {
            alive =false;
            attacking = false;
            shooting = false;
            animator.SetBool("Bow", false);
            animator.SetBool("Attack", false);
            navMesh.isStopped = true;
            animator.Play("Death", 0);
            animator.Play("Death", 1);
            GetComponent<CapsuleCollider>().enabled = false;
            GameObject arrow = Instantiate(spawnPickUp, transform.position , Quaternion.identity);
            Destroy(gameObject, healthbar.deSpawnTime);
        }
    }

    private void Update()
    {
        if(alive)
        {
            animator.SetFloat("Blend", navMesh.velocity.magnitude);
            if(player!=null)
            {
                direction = player.transform.position - transform.position;
                Ray ray = new Ray(transform.position, direction);
                RaycastHit hit;

                // Perform the raycast
                if (TP_PlayerController.current.alive && Physics.Raycast(ray, out hit, 10f, EnemyManager.current.visionLayer))
                {
                    if (hit.collider.gameObject.layer == 13)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 5f * Time.deltaTime);
                        float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);

                        if( distance >= EnemyManager.current.maxDistance)
                        {
                            navMesh.updateRotation = true;
                            navMesh.isStopped = false;
                            navMesh.SetDestination(player.transform.position);
                            // animator.SetBool("Bow", false);
                            animator.SetBool("Attack", false);
                        }
                        else if(distance <= EnemyManager.current.minDistance && distance > 2f){

                            navMesh.isStopped = false;
                            navMesh.SetDestination(-direction.normalized * 20f);
                            animator.SetBool("Bow", false);
                            animator.SetBool("Attack", false);
                        }
                        else if(distance <= 2f){
                            navMesh.updateRotation = false;
                            navMesh.isStopped = false;
                            animator.SetBool("Attack", true);
                        }
                        else{

                            navMesh.isStopped = true;
                            animator.SetBool("Bow", true);
                            animator.SetBool("Attack", false);
                            
                        }
                    }
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10f, Color.blue);
                }
                else
                {
                    navMesh.isStopped = false;
                    animator.SetBool("Bow", false);
                    animator.SetBool("Attack", false);
                }
            }
        }
    }

    public void Shoot()
    {
        Vector3 newDir = new Vector3(direction.x + Random.Range(-0.1f,0.1f), direction.y+ Random.Range(-0.5f,0.5f) ,direction.z );
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawn.position , Quaternion.LookRotation(newDir));
    }

    public void EndAttack()
    {
        attacking = false;
    }
    public void Attacking()
    {
        hitscan.CheckHit();

    }
}
