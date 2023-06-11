using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] private LayerMask hitLayers;
    [SerializeField] float hitRadius = 0.2f;

    public void CheckHit()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, hitRadius, hitLayers);
            
        foreach (Collider collider in colliders){
            if(collider.TryGetComponent<TP_PlayerController>(out TP_PlayerController player))
            {
                if(player.alive)
                {
                    if(!TP_PlayerController.current.blocked)
                    {
                        DamageManager.Damage(TP_PlayerController.current.healthbar,0.1f);
                        TP_PlayerController.current.particles.Play();
                    }
                }
                
            }

        }
    }
}
