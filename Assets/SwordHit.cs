using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class SwordHit : MonoBehaviour
{
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] private ParticleSystem particles;
    [SerializeField] float hitRadius = 0.2f;
    [SerializeField] bool Active =false;
    public static SwordHit current;

    void Awake()
    {
        current = this;
    }

    public void CheckHit()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, hitRadius, hitLayers);
            
        foreach (Collider collider in colliders){
            if(collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                if(enemy.alive)
                {
                    if(!TP_PlayerController.current.blocked)
                    {
                        DamageManager.Damage(enemy.healthbar);
                        enemy.animator.Play("Hurt", 1);
                        particles.Play();
                    }
                    else
                    {
                        if(!TP_PlayerController.current.animator.GetBool("moving"))
                        {
                            DamageManager.Damage(enemy.healthbar,0.1f);
                            enemy.animator.Play("Hurt", 1);
                        }
                    }
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
