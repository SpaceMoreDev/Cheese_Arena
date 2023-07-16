using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCam;
    private CinemachinePOV povCamera;
    private CinemachineFramingTransposer framingCamera;

    [Header("Settings")]
    [SerializeField] [Range(0.01f, 5f)] private float sensitivity = 0.1f;
    [SerializeField] [Range(1f, 5f)] private float distanceFromPlayer = 2.2f;

    private void Start() {
        // _virtualCam = GetComponentInChildren<CinemachineVirtualCamera>();
        if(_virtualCam == null){return;}
        povCamera = _virtualCam.GetCinemachineComponent<CinemachinePOV>();
        framingCamera = _virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();

        povCamera.m_VerticalAxis.m_MaxSpeed = sensitivity;
        povCamera.m_HorizontalAxis.m_MaxSpeed = sensitivity;

        framingCamera.m_CameraDistance = distanceFromPlayer;
    }
}
