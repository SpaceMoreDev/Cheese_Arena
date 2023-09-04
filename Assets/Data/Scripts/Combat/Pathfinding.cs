using UnityEngine;
using UnityEngine.AI;

public class Pathfinding : MonoBehaviour
{
    Vector3 direction = Vector3.zero;
    float magnitude = 0f;
    Rigidbody _rigidbody;
    NavMeshAgent _nav;
    Transform Player;


    [SerializeField] float _speed = 2f;
    
    private void Start() {
        Player = PlayerMovement.current.gameObject.transform;
        _rigidbody = GetComponent<Rigidbody>();
        _nav = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        direction = Player.position - transform.position;
        magnitude = Vector3.Distance(transform.position, Player.position);

        var lookDir = direction;
        lookDir.y = 0; // keep only the horizontal direction
        transform.rotation = Quaternion.LookRotation(lookDir);
    }
    private void FixedUpdate() {
        float i = Mathf.InverseLerp(1.5f, 5, magnitude);
        // Debug.Log(i);

        if(i>0.3f)
            _nav.destination = Player.position;

        // _rigidbody.MovePosition(transform.position + direction * Time.fixedDeltaTime * i * _speed);

    }
}
