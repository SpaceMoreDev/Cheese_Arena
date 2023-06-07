using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Managers
{
    public class DamageManager : MonoBehaviour
    {
        static Action<float> damagedAction;
        static float damage = 0.1f;
        
        public static void Damage(Health health)
        {
            if(damagedAction != null)
            {
                damagedAction(damage);
            }
            health.DecreaseHealth(damage);
        }
    }
}
