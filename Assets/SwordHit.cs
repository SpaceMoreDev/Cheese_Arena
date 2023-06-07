using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] float hitRadius = 0.2f;
    [SerializeField] bool Active =false;
    void Update()
    {
        if(Active)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, hitRadius, hitLayers);
                
            foreach (Collider collider in colliders){
                if(collider.TryGetComponent<Enemy>(out Enemy enemy))
                {
                   DamageManager.Damage(enemy.healthbar);
                }

            }
        }
        
    }

     void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }
}
