using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public Image healthBar;

    public void DecreaseHealth(float value)
    {
        healthBar.fillAmount -= value;
        Debug.Log(healthBar.fillAmount);
    }
    public void IncreaseHealth(float value)
    {
        healthBar.fillAmount += value;
        Debug.Log(healthBar.fillAmount);
    }

    private void LateUpdate()
    {
        Transform cameraTransform = Camera.main.transform;
        transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
    }
}
