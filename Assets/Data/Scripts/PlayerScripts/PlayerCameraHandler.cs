using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCameraHandler : MonoBehaviour
{
    [SerializeField] internal Camera camera;
    [SerializeField] private CinemachineVirtualCamera _virtualCam;
    private CinemachinePOV povCamera;
    private CinemachineFramingTransposer framingCamera;
    private static PlayerCameraHandler _instance;

    [Header("Settings")]
    [SerializeField] [Range(0.01f, 5f)] private float sensitivity = 0.1f;
    [SerializeField] [Range(1f, 5f)] private float distanceFromPlayer = 2.2f;

    internal Vector2  input = Vector2.zero;
    public static PlayerCameraHandler Instance{
        get{
            if(_instance == null)
            {
                return new PlayerCameraHandler();
            }
            return _instance;
        }
    }
    private void Awake() {
        _instance = this;
    }
    private void Start() {
        // _virtualCam = GetComponentInChildren<CinemachineVirtualCamera>();
        if(_virtualCam == null){return;}
        povCamera = _virtualCam.GetCinemachineComponent<CinemachinePOV>();
        framingCamera = _virtualCam.GetCinemachineComponent<CinemachineFramingTransposer>();

        povCamera.m_VerticalAxis.m_MaxSpeed = sensitivity;
        povCamera.m_HorizontalAxis.m_MaxSpeed = sensitivity;

        framingCamera.m_CameraDistance = distanceFromPlayer;
    }
    public void SetCamerPOV(bool state)
    {
        povCamera.enabled = state;
    }
}
