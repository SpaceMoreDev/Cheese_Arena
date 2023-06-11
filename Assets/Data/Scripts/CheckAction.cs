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
    private bool consumed = false;
    Collider[] precol;

    void Awake()
    {
        Select = InputManager.inputActions.General.Interact;
        precol = new Collider[]{};
    }
    void OnEnable()
    {
        Select.performed += _ => SelectClick();
    }
    void OnDestroy()
    {
        Select.performed -= _ => SelectClick();
    }

    

    void SelectClick()
    {
        if(TP_PlayerController.current.alive)
        {
            int count = 0;
            foreach (Collider collider in precol){
                float distance = Vector3.Distance(transform.position, collider.transform.position);            
                if(collider.TryGetComponent<pickup_Health>(out pickup_Health pickup))
                {
                    pickup.checkToConsume();
                }
                count++;
            }
            consumed = true;
            // Debug.Log($"precol: {precol.Length}");
        }
    }

    void Update()
    {
        if(TP_PlayerController.current.alive)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, actionRadius, actionLayers);
                
            foreach (Collider collider in colliders){
                float distance = Vector3.Distance(transform.position, collider.transform.position);            
                if(collider.TryGetComponent<pickup_Health>(out pickup_Health pickup))
                {
                    pickup.selectionUI.SetActive(true);
                    consumed = pickup.consumed;
                }
            }
            if(colliders.Length == 0 && precol.Length != 0)
            {
                if(!consumed)
                {
                foreach (Collider collider in precol){            
                    if(collider.TryGetComponent<pickup_Health>(out pickup_Health pickup))
                    {
                        pickup.selectionUI.SetActive(false);
                    }
                }
                }
            }
            precol = colliders;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, actionRadius);
    }
}
