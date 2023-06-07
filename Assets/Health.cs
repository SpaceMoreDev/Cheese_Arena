using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] Image healthBar;

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
}
