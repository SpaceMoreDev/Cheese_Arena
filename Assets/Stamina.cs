using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField] public Image staminaBar;
    [HideInInspector] public GameObject character;
    float regenWait = 0.5f;
    [SerializeField] public float increaseRate = 0.1f;
    public static Action<GameObject> staminaBarEmpty;
    bool regen = true;

    public void DecreaseStamina(float value)
    {
        staminaBar.fillAmount -= value;

        if(staminaBar.fillAmount<=0)
        {
            if(staminaBarEmpty != null)
            {
                staminaBarEmpty(character);
            }
        }
        regen = false;
        StartCoroutine(WaitAndRegen());
        // Debug.Log(healthBar.fillAmount);
    }
    public void IncreaseStamina(float value)
    {
        staminaBar.fillAmount += value;
        // Debug.Log(healthBar.fillAmount);
    }

    private IEnumerator WaitAndRegen()
    {
        
        yield return new WaitForSeconds(regenWait); // Wait for 3 seconds
        regen = true;
    }

    void Update()
    {
        if(TP_PlayerController.current.sprinting)
        {
            if(staminaBar.fillAmount > 0)
            {
                staminaBar.fillAmount -= increaseRate * Time.deltaTime;
            }
        }
        else if (regen)
        {
            
            if(staminaBar.fillAmount < 1)
            {
                staminaBar.fillAmount += increaseRate * Time.deltaTime;
            }
        }
    }
}
