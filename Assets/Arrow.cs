using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    
    public static float destroyTime = 5f;
    private Vector3 initialScale;
    private Vector3 originalLocalScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        originalLocalScale = transform.localScale; // Store the original local scale
    }
    void Start()
    {
        rb.velocity = transform.forward * EnemyManager.current.arrowSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            return; // Ignore collisions with player and other arrows
        }
        if(collision.gameObject.layer == 9)
        {
            DamageManager.Damage(TP_PlayerController.current.healthbar, 0.3f);
            Debug.Log("ouch!!");
        }

        gameObject.transform.SetParent(collision.transform, true);

        // Disable physics interaction
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        // Destroy the arrow after the specified time
        Destroy(gameObject, destroyTime);
        
    }
}
