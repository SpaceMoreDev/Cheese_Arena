using System;
using System.Collections.Generic;
using UnityEngine;
namespace Managers
{
    public class HealthManager : MonoBehaviour
    {
        [SerializeField] public Health healthbar;
        GameObject healthObject;
        [HideInInspector] public bool visible = false;

        // Start is called before the first frame update
        void Start()
        {
            healthObject = this.gameObject;
        }

        public void SetVisible(bool isvisible)
        {
            visible = isvisible;
            healthbar.gameObject.SetActive(isvisible);
        }
    }
}