using System.Collections;
using Managers;
using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;

public class CheckAction : MonoBehaviour
{
    [SerializeField] private LayerMask actionLayers;
    [SerializeField] GameObject selectionUI;
    [SerializeField] float actionRadius = 3.5f;
    private static InputAction Select;
    private bool showUI = false;

    void Awake()
    {
        Select = InputManager.inputActions.General.Interact;
    }
    void OnEnable()
    {
        Select.performed += _ => SelectClick();
    }
    void OnDestroy()
    {
        Select.performed -= _ => SelectClick();
    }

    Collider currentActive;
    void Update()
    {
        float minimumDistance = Mathf.Infinity;
        Collider[] colliders = Physics.OverlapSphere(transform.position, actionRadius, actionLayers);
            
        foreach (Collider collider in colliders){
            float distance = Vector3.Distance(transform.position, collider.transform.position);            
            
            currentActive = null;
            if(distance <= minimumDistance)
            {
                minimumDistance = distance;
                currentActive = collider;
            }
        }

        if(currentActive != null)
        {
            Debug.DrawLine(transform.position, currentActive.transform.position);
            
            if(PlayerInputHandler.Interact.triggered)
            {
                NotificationManager.StartNotification($"Touched {currentActive.name}");
                
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, actionRadius);
    }
    private void SelectClick()
    {
        Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0f);
        Ray ray = Camera.main.ViewportPointToRay(screenCenter);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Access the hit object
            GameObject hitObject = hit.collider.gameObject;
            // Example: Print the name of the hit object
            
           
            // Debug.Log("Hit: " + hitObject.name);
        
            // if(hitObject.TryGetComponent<IActivites>(out IActivites obj))
            // {
            //     obj.Activate(TP_PlayerController.current.playerCharacter, obj.ID);
            // }
        }
    }
}
