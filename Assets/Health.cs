using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public Image healthBar;
    [HideInInspector] public GameObject character;
    [SerializeField] public float deSpawnTime = 2f;
    public static Action<GameObject> healthBarEmpty;

    public void DecreaseHealth(float value)
    {
        healthBar.fillAmount -= value;

        if(healthBar.fillAmount<=0)
        {
            if(healthBarEmpty != null)
            {
                healthBarEmpty(character);
            }
            if(character.TryGetComponent<TP_PlayerController>(out TP_PlayerController found))
            {

            }
            else
            {
                Destroy(character, deSpawnTime);
            }
        }
        // Debug.Log(healthBar.fillAmount);
    }
    public void IncreaseHealth(float value)
    {
        healthBar.fillAmount += value;
        // Debug.Log(healthBar.fillAmount);
    }


    private void LateUpdate()
    {
        Transform cameraTransform = Camera.main.transform;
        transform.LookAt(transform.position + cameraTransform.rotation * Vector3.forward, cameraTransform.rotation * Vector3.up);
    }
}
