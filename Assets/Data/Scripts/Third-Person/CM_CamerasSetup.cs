using Cinemachine;
using UnityEngine;

namespace Managers
{
    public class CM_CamerasSetup : MonoBehaviour
    {
        static CM_CamerasSetup current;

        void Awake()
        {
            current = this;
            FocusMouse(true);
        }

        public static void FocusMouse(bool condition)
        {
            if(condition)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }else{
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;}
        }

        public static void SetTargetLook(Transform player)
        {
            var list = current.GetComponentsInChildren<CinemachineVirtualCamera>();
            foreach(var i in list)
            {
                i.LookAt = player;
                i.m_LookAt = player;
            }
        }

        public static void SetTargetFollow(Transform player)
        {
            var list = current.GetComponentsInChildren<CinemachineVirtualCamera>();
            foreach(var i in list)
            {
                i.Follow = player;
                i.m_Follow = player;
            }
        }

        public static void PauseCamera(bool isPaused)
        {
            var list = current.GetComponentsInChildren<CinemachineVirtualCamera>();
            if(isPaused)
            {
                foreach(var i in list)
                {
                    
                    i.enabled = false;
                    // CinemachineOrbitalTransposer orbitalTransposer = i.GetCinemachineComponent<CinemachineOrbitalTransposer>();
                    // orbitalTransposer.m_YawDamping = float.MaxValue;
                }
            }
            else
            {
                foreach(var i in list)
                {
                    i.enabled = true;
                    // CinemachineOrbitalTransposer orbitalTransposer = i.GetCinemachineComponent<CinemachineOrbitalTransposer>();
                    // orbitalTransposer.m_YawDamping = float.MinValue;
                }
            }
        }
    }
}
