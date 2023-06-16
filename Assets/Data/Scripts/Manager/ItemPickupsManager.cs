using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class ItemPickupsManager : MonoBehaviour
    {
        public static ItemPickupsManager current;
        [SerializeField] public List<GameObject> pickupObjects;

        void Awake()
        {
            current = this;
        }
    }
}