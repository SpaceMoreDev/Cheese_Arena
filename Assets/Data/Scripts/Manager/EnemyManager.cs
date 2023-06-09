using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] public GameObject player;
        [SerializeField] public LayerMask visionLayer;
        public float maxDistance = 10f;
        public float minDistance = 5f;
        public float shootInterval = 1f;
        public float arrowSpeed = 10f;
        
        public static EnemyManager current;
        void Awake()
        {
            current = this;
        }
    }
}
