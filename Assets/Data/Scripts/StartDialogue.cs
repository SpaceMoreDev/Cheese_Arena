using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class StartDialogue : MonoBehaviour
{
    [SerializeField] CharacterObject characterObject;
    [SerializeField] Vector3 boxRect = Vector3.zero;
    [SerializeField] private LayerMask actionLayers;
    [SerializeField] bool activated = false;
    
    void OnEnable()
    {
        DialogueManager.EndDialogueAction += EndDialogue;
    }

    void EndDialogue()
    {
        CM_CamerasSetup.PauseCamera(false);
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position, boxRect, Quaternion.identity, actionLayers);

        foreach (Collider collider in colliders){

            if(!activated)
            {

                CM_CamerasSetup.PauseCamera(true);

                DialogueManager.StartDialogue(gameObject, characterObject, 0);
                Debug.Log($"PLAYER ENTERED {characterObject.CharacterName}");
                activated = true;
                
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxRect);
    }
}
