using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using  Cinemachine;
using Managers;
public class CutscenesManager : MonoBehaviour
{
    [SerializeField] Canvas PlayerCanvas;
    [SerializeField] public CinemachineVirtualCamera main_camera;

    public void CutsceneEnd()
    {
        PlayerCanvas.gameObject.SetActive(true);
        CM_CamerasSetup.SetTargetLook(TP_PlayerController.current.transform);
        CM_CamerasSetup.FocusMouse(true);
        MainMenu.playing = true;
    }
}
