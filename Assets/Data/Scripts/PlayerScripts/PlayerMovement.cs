using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Managers;
using Behaviours;

public enum PlayerState{
    Unfocused,
    Gameplay
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
        Movement.Rotate(input);
    }
    Vector3 rootMotion;
    /// <summary>
    /// this function is called when the animator moves the object via rootmotion
    /// </summary>
    private void OnAnimatorMove() {
        _controller.Move(_animator.deltaPosition);
    }

    private void OnGround() {
        if(Movement.IsSprinting){ _stamina.Bar.Decrease(Time.deltaTime * 0.1f); } 

        // ----- Animation -----
        Vector2 movementVelocity;
        if(Movement.IsSprinting){
            movementVelocity.y = Mathf.Clamp(input.y,-1f,1f);
            movementVelocity.x = Mathf.Clamp(input.x,-1f,1f);

        }else{
            movementVelocity.y = Mathf.Clamp(input.y,-0.5f, 0.5f);
            movementVelocity.x = Mathf.Clamp(input.x,-0.5f, 0.5f);
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
