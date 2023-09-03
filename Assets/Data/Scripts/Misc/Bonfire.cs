using System.Collections;
using System;
using UnityEngine;
using Managers;

public class Bonfire : MonoBehaviour , I_ActivateActions
{
    [SerializeField] private GameObject selectionUI;
    public GameObject DisplayUI {get{return selectionUI;}}
    public static Action<Bonfire> playerSelectedBonfire;

    // Start is called before the first frame update
    private bool showUI = false;
    private bool activated = false;
    static Bonfire activeCheckpoint;
    public bool Activated {get{return activated;}}

    void OnEnable()
    {
        playerSelectedBonfire += this.checkBonfire;
    }
    void OnDisable()
    {
        playerSelectedBonfire -= this.checkBonfire;
    }

    void checkBonfire(Bonfire chosen)
    {
        if(chosen == this)
        {
            this.activated = true;
        }
        else
        {
            this.activated = false;
        }
    }

    public void Activate()
    {
        if(!this.activated && this != activeCheckpoint)
        {
            NotificationManager.StartNotification("Activated checkpoint");
            PlayerRespawnManager.SetRespawn(gameObject.transform);
            activeCheckpoint = this; 
            playerSelectedBonfire?.Invoke(activeCheckpoint);
        }
        else
        {
            NotificationManager.StartNotification("Aleady Activated THIS checkpoint..");
        }
    }
    
    private void LateUpdate()
    {
        Transform cameraTransform = Camera.main.transform;
        selectionUI.transform.LookAt(selectionUI.transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
    }
}
