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
        TP_PlayerController.current.staminabar.DecreaseStamina(TP_PlayerController.current.attackStamina);
        Collider[] colliders = Physics.OverlapSphere(transform.position, hitRadius, hitLayers);
            
        foreach (Collider collider in colliders){
            if(collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                if(enemy.alive)
                {
                    if(!TP_PlayerController.current.blocked)
                    {
                        DamageManager.Damage(enemy.health.healthbar);
                        enemy.animator.Play("Hurt", 1);
                        particles.Play();
                    }
                    else
                    {
                        TP_PlayerController.current.staminabar.DecreaseStamina(TP_PlayerController.current.blockStamina);
                        if(!TP_PlayerController.current.animator.GetBool("moving"))
                        {
                            DamageManager.Damage(enemy.health.healthbar,0.1f);
                            enemy.animator.Play("Hurt", 1);
                        }
                    }
                }
                
            }
            else if (collider.TryGetComponent<Destructable>(out Destructable destructable))
            {
                DamageManager.Damage(destructable.health.healthbar,0.4f);
            }

            if(collider.TryGetComponent<HealthManager>(out HealthManager health))
            {
                health.SetVisible(true);
            }

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }
}
