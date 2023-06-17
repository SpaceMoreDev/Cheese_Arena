using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class Bonfire : MonoBehaviour , ActivateActions
{
    [SerializeField] private GameObject selectionUI;
    public GameObject DisplayUI {get{return selectionUI;}}

    // Start is called before the first frame update
    private bool showUI = false;
    private bool consumed = false;
    public bool Activated {get{return consumed;}}

    public void Activate()
    {
        NotificationManager.StartNotification("Activated Bonfire");
        PlayerRespawnManager.SetRespawn(gameObject.transform.position);
    }
    
    private void LateUpdate()
    {
        Transform cameraTransform = Camera.main.transform;
        selectionUI.transform.LookAt(selectionUI.transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
    }
}
