using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public Health healthbar;
    void FixedUpdate()
    {
        // healthbar.DecreaseHealth(0.1f * Time.fixedDeltaTime);
    }
}
