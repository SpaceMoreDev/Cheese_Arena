using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using  Cinemachine;
using Managers;
public class CutscenesManager : MonoBehaviour
{
    [SerializeField] public CinemachineVirtualCamera main_camera;
    public static CutscenesManager current;
    void Awake()
    {
        current =this;
    }
    public void CutsceneEnd()
    {
        UIManager.current.PlayerCanvas.gameObject.SetActive(true);
        CM_CamerasSetup.SetTargetLook(TP_PlayerController.current.transform);
        CM_CamerasSetup.FocusMouse(true);
        MainMenu.playing = true;
        TP_PlayerController.current.PlayerLight.gameObject.SetActive(true);
    }
}
