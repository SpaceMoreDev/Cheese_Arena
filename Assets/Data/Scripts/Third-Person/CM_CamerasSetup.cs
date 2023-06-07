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

        public static void SetTargetLook(TP_PlayerController player)
        {
            var list = current.GetComponentsInChildren<CinemachineVirtualCamera>();
            foreach(var i in list)
            {
                i.m_LookAt = player.transform;
            }
        }

        public static void SetTargetFollow(TP_PlayerController player)
        {
            var list = current.GetComponentsInChildren<CinemachineVirtualCamera>();
            foreach(var i in list)
            {
                i.m_Follow = player.transform;
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
                }
            }
            else
            {
                foreach(var i in list)
                {
                    i.enabled = true;

                }
            }
        }
    }
}
