using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Health healthbar;
    void FixedUpdate()
    {
        if(healthbar.healthBar.fillAmount<=0)
        {
            Destroy(gameObject);
        }
    }
}
