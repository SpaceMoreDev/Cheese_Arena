using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    [SerializeField] float actionRadius = 3.5f;
    [SerializeField] CharacterObject characterObject;
    [SerializeField] Transform lookTarget;
    [SerializeField] Vector3 boxRect = Vector3.zero;
    [SerializeField] private LayerMask actionLayers;
    GameObject currentActive;
    
    void OnEnable()
    {
        DialogueManager.EndDialogueAction += EndDialogue;
    }

    void EndDialogue()
    {
        CM_CamerasSetup.SetTargetFollow(TP_PlayerController.current.transform);
        CM_CamerasSetup.SetTargetLook(TP_PlayerController.current.transform);
        CM_CamerasSetup.PauseCamera(false);

    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, boxRect, Quaternion.identity, actionLayers);

        foreach (Collider collider in colliders){

            if(currentActive != collider.gameObject)
            {
                if(collider.gameObject == TP_PlayerController.current.gameObject)
                {
                    currentActive = collider.gameObject;
                    CM_CamerasSetup.SetTargetLook(lookTarget);
                    CM_CamerasSetup.PauseCamera(true);

                    DialogueManager.StartDialogue(gameObject, characterObject, 0);
                    // Debug.Log($"PLAYER ENTERED {characterObject.CharacterName}");
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxRect);
    }
}
