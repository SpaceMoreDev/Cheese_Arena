using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Managers;
using Behaviours;
using System.Data;

public enum PLAYER_STATE{
    UNFOCUSED,
    COMBAT,
    GAMEPLAY
}
public class PlayerMovement : MonoBehaviour
{
    [Header("General")]
    // serialized variables
    [SerializeField] [Range(1f,10f)] private float playerSpeed = 2.0f;
    [SerializeField] [Range(1,10)] private int sprintMultiplier = 2;
    [SerializeField] [Range(0.5f,10f)] float turningSpeed = 5f;
    [SerializeField][Range(0f, 20f)] float gravityValue = 18.81f;
    [SerializeField] internal bool canMove = true;
    [SerializeField] internal bool IsRunning = false;
    [SerializeField] [Range(0.5f, 1f)] protected float limitMotion = 0.5f;
    public static PlayerMovement current;
 
    [Header("Animation")]
    [SerializeField] [Range(0.01f,1f)] private float animationBlend = 0.05f;

    // private variables
    private float tempSpeed = 5f;
    private Transform cameraTransform;
    private Movement _movement;
    private StaminaBar _stamina;
    private HealthBar _health;
    internal Vector3 playerVelocity = Vector3.zero;


    //component references
    internal CharacterController _controller;
    private Animator _animator;
    private PLAYER_STATE _state = PLAYER_STATE.GAMEPLAY;
    internal PLAYER_STATE playerState {
        set{
            _state = value;
            if(_state == PLAYER_STATE.COMBAT){
                _animator.SetBool("Combat", true);
            }
            else{
                _animator.SetBool("Combat", false);
            }
            
            NotificationManager.StartNotification(_state.ToString());
        }
        get{
            return _state;
        }
    }

    internal Vector2  input = Vector2.zero;
    public Movement Movement{
        get{
            if(_movement != null)
            {
                return _movement;;
            }
            _movement = new Movement(_controller, 
                                    playerSpeed, 
                                    gravityValue, 
                                    cameraTransform,
                                    turningSpeed
                                    );
            return _movement;
        }
    }

    private void Awake() {
        current = this;
        InputManager.ToggleActionMap(InputManager.inputActions.General);
        _stamina = GetComponent<StaminaBar>();
        _health = GetComponent<HealthBar>();
        _animator = GetComponent<Animator>();

        InputManager.inputActions.General.Move.performed += this.PlayerInput;
        InputManager.inputActions.General.Move.canceled += this.PlayerInput;
        InputManager.inputActions.General.Sprint.started += _ => {
            Movement.Speed *= sprintMultiplier;
            _animator.speed *= sprintMultiplier/2;
            Movement.IsSprinting = true;
            };
        InputManager.inputActions.General.Sprint.canceled += _ => {
            Movement.Speed = playerSpeed;
            _animator.speed = 1;
            Movement.IsSprinting = false;
            };
         _controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        _stamina.Bar.BarIsEmpty += SprintEnd;

    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerState = PLAYER_STATE.GAMEPLAY;
    }
    internal void SprintEnd()
    {
        Movement.Speed = playerSpeed;
        _animator.speed = 1;
        Movement.IsSprinting = false;
    }

    /// <summary>
    /// This function is responsible for input vector2 movements
    /// </summary>
    /// <param name="ctx"></param>
    private void PlayerInput(InputAction.CallbackContext ctx)
    {
        if(ctx.performed){ input = ctx.ReadValue<Vector2>(); }
        else if(ctx.canceled){ input = Vector2.zero; }
    }
    private void Update() {
        // if(_state == PLAYER_STATE.GAMEPLAY)
        // {
            Movement.Rotate(input);
        // }
    }
    Vector3 rootMotion;
    /// <summary>
    /// this function is called when the animator moves the object via rootmotion
    /// </summary>
    private void OnAnimatorMove() {
        _controller.Move(_animator.deltaPosition);
        if(playerState != PLAYER_STATE.GAMEPLAY){
            if(PlayerCameraHandler.Instance.ActiveTarget != null){
                Vector3 direction = PlayerCameraHandler.Instance.ActiveTarget.transform.position - _controller.transform.position;
                direction.y = 0f;
                
                _controller.transform.forward = direction;
            }    
        }
       
    }
    
    private void OnGround() {
        if(Movement.IsSprinting){ _stamina.Bar.Decrease(Time.deltaTime * 0.1f); } 

        // ----- Animation -----
        Vector2 movementVelocity;
        if(Movement.IsSprinting){
            movementVelocity.y = Mathf.Clamp(input.y,-1f,1f);
            movementVelocity.x = Mathf.Clamp(input.x,-1f,1f);

        }else{
            movementVelocity.y = Mathf.Clamp(input.y,-limitMotion, limitMotion);
            movementVelocity.x = Mathf.Clamp(input.x,-limitMotion, limitMotion);
        }
        _animator.SetFloat("Blend_x",movementVelocity.x, animationBlend, Time.deltaTime);
        _animator.SetFloat("Blend_y",movementVelocity.y, animationBlend, Time.deltaTime);
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// /// </summary>
    void FixedUpdate()
    {
        OnGround();
    }
}
