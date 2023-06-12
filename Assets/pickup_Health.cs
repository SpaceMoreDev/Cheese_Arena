using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class pickup_Health : MonoBehaviour
{
    public GameObject selectionUI;
    // Start is called before the first frame update
    private bool showUI = false;
    public bool consumed = false;

    public void checkToConsume()
    {
        if(!consumed)
        {
            NotificationManager.StartNotification("Consumed the Cheese..");
            TP_PlayerController.current.healthbar.IncreaseHealth(0.25f);
            Destroy(gameObject);
            consumed = true;
        }
    }
    
    private void LateUpdate()
    {
        Transform cameraTransform = Camera.main.transform;
        selectionUI.transform.LookAt(selectionUI.transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
    }
}
