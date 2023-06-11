using System.Collections;
using System.Collections.Generic;
using System;
using Cinemachine;
using UnityEngine;

namespace Managers
{
    public class DamageManager : MonoBehaviour
    {
        static float damage = 0.1f;
        
        public static void Damage(Health health)
        {
            health.DecreaseHealth(damage);
        }

        public static void Damage(Health health, float customDamage)
        {
            health.DecreaseHealth(customDamage);
        }
    }
}
