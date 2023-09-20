using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Managers;
using MyBox;
using System;
using System.Linq;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerCameraHandler : MonoBehaviour
{
    [SerializeField] internal Camera camera;
    [SerializeField] private CinemachineVirtualCamera _followCam;
    [SerializeField] private CinemachineVirtualCamera _targetCam;
    [SerializeField] private Animator _camStateAnim;
    private CinemachinePOV povCamera;
    private CinemachineFramingTransposer framingCamera;
    private static PlayerCameraHandler _instance;

    [Header("Settings")]
    [SerializeField] [Range(0.01f, 5f)] private float sensitivity = 0.1f;
    [SerializeField] [Range(1f, 5f)] private float distanceFromPlayer = 2.2f;
    [SerializeField] private RectTransform LockOnSprite;
    [SerializeField] private float LockOnRadius = 10f;

    [SerializeField] private LayerMask layerMask = 1 << 8;
    public static bool isLockOn =false;
    internal GameObject ActiveTarget;
    private List<GameObject> ActiveTargets;

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
        if(_followCam == null){return;}
        povCamera = _followCam.GetCinemachineComponent<CinemachinePOV>();
        framingCamera = _followCam.GetCinemachineComponent<CinemachineFramingTransposer>();

        povCamera.m_VerticalAxis.m_MaxSpeed = sensitivity;
        povCamera.m_HorizontalAxis.m_MaxSpeed = sensitivity;

        framingCamera.m_CameraDistance = distanceFromPlayer;
        InputManager.inputActions.General.LockOn.started += _ =>ChangeCamera();
        InputManager.inputActions.General.Scroll.performed += ChangeTarget;
    }
    void ChangeTarget(InputAction.CallbackContext ctx)
    {
        if(ctx.ReadValue<float>() > 0)
        {
            if(isLockOn)
            {
                CheckTargets(false);
                if(ActiveTargets.Count > 1)
                {
                    int activeIndex = 0;
                    Debug.Log($"number of targets {ActiveTargets.Count}");
                    if(ActiveTargets[activeIndex+1] != null )
                    {
                        ActiveTarget = ActiveTargets[activeIndex+1];
                        _targetCam.m_LookAt = ActiveTarget.transform;
                        return;
                    }
                    
                }
            }
        }
    }
    void ChangeCamera()
    {
        if(isLockOn){
            isLockOn = false;
            PlayerMovement.current.playerState = PLAYER_STATE.GAMEPLAY;
            _camStateAnim.Play("PlayerCam");
            LockOnSprite.parent.gameObject.SetActive(false);
            ActiveTarget = null;
        }else{
            if(CheckTargets()){
                isLockOn = true;
                PlayerMovement.current.playerState = PLAYER_STATE.COMBAT;
                _camStateAnim.Play("TargetCam");
                Vector3 dir = ActiveTarget.transform.position - transform.position;
                dir.y = 0;
                transform.forward = dir;

                LockOnSprite.parent.gameObject.SetActive(true);
            }
        }
    }
    public void SetCamerPOV(bool state)
    {
        povCamera.enabled = state;
    }
    private bool CheckTargets(bool setTarget = true) {
        float min = 99999;
        bool active = false;
        
        Collider[] targets = Physics.OverlapSphere(transform.position,LockOnRadius,layerMask);

        ActiveTargets = new List<GameObject>();

        if(ActiveTarget != null){ // to make sure that the active target always remains first
            ActiveTargets.Add(ActiveTarget);
        }

        foreach(var target in targets){
            if(target.gameObject == ActiveTarget){continue;}
            
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if(setTarget){
                if(distance < min){   
                    ActiveTarget = target.gameObject;
                    min = distance;
                    active = true;
                } 
            }

            ActiveTargets.Add(target.gameObject); 
        }
        if(active)
        {
             if(setTarget){
                _targetCam.m_LookAt = ActiveTarget.transform;
             }
            return true;
        }
        return false;
    }
    private void Update() {
        if(isLockOn){ // locked on target

            LockOnSprite.position = Camera.main.WorldToScreenPoint(ActiveTarget.transform.position);
            Vector3 dir = ActiveTarget.transform.position - transform.position;
            Debug.DrawRay(transform.position, dir, Color.green);
            Ray ray = new Ray(transform.position, dir);
            float distance = Vector3.Distance(transform.position, ActiveTarget.transform.position);

            if (distance > LockOnRadius)
            {
                // _camStateAnim.Play("PlayerCam");
                // LockOnSprite.parent.gameObject.SetActive(false);
                // PlayerMovement.current.playerState = PLAYER_STATE.GAMEPLAY;
                // isLockOn = false;
                ChangeCamera();
            }
        }else{
            if(ActiveTarget != null){
                Vector3 dir = ActiveTarget.transform.position - transform.position;
                Debug.DrawRay(transform.position, dir, Color.red);
            }
        }
    }
}
