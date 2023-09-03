using System.Collections;
using Managers;
using UnityEngine.InputSystem;
using UnityEngine;
using Cinemachine;

public class CheckAction : MonoBehaviour
{
    [SerializeField] private LayerMask actionLayers;
    [SerializeField] private float actionRadius = 3.5f;


    private static InputAction Select;
    private static InputAction SelectUI;
    private bool consumed = false;
    private Collider[] precol;

    void Awake()
    {
        Select = InputManager.inputActions.General.Interact;
        SelectUI = InputManager.inputActions.UI.Interact;
        precol = new Collider[]{};
    }
    void OnEnable()
    {
        Select.performed += _ => SelectClick();
        SelectUI.performed += _ => SelectClick();
    }

    void OnDestroy()
    {
        Select.performed -= _ => SelectClick();
        SelectUI.performed -= _ => SelectClick();

    }

    void SelectClick()
    {
        int count = 0;
        foreach (Collider collider in precol){
            float distance = Vector3.Distance(transform.position, collider.transform.position);            
            if(collider.TryGetComponent<I_ActivateActions>(out I_ActivateActions action)){
                action.Activate(GetComponent<Character>());
                action.DisplayUI.SetActive(false);
            }
            count++;
        }
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, actionRadius, actionLayers);
            
        foreach (Collider collider in colliders){
            float distance = Vector3.Distance(transform.position, collider.transform.position);            
            if(collider.TryGetComponent<I_ActivateActions>(out I_ActivateActions pickup))
            {
                if(!pickup.Activated)
                {
                    pickup.DisplayUI.SetActive(true);
                }
            }
        }
        if(colliders.Length == 0 && precol.Length != 0)
        {

            foreach (Collider collider in precol){       
                if(collider != null)
                {     
                    if(collider.TryGetComponent<I_ActivateActions>(out I_ActivateActions pickup))
                    {
                        pickup.DisplayUI.SetActive(false);
                    }
                }
            }
        }
        precol = colliders;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, actionRadius);
    }
}
