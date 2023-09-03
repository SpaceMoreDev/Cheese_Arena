using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Rotation : MonoBehaviour
{
    private void LateUpdate()
    {
        Transform cameraTransform = Camera.main.transform;
        transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
    }
}
