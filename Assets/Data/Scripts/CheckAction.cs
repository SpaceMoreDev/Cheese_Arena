using System.Collections;
using Managers;
using UnityEngine;

public class CheckAction : MonoBehaviour
{
    [SerializeField] private LayerMask actionLayers;
    [SerializeField] float actionRadius = 3.5f;

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, actionRadius, actionLayers);
        foreach (Collider collider in colliders){
            if(PlayerInputHandler.Interact.triggered)
            {
                NotificationManager.StartNotification($"Touched {collider.name}");
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, actionRadius);
    }
}
