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

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("General")]
    // serialized variables
    [SerializeField] [Range(1f,10f)] private float playerSpeed = 2.0f;
    [SerializeField] [Range(1,10)] private float sprintMultiplier = 2.0f;
    [SerializeField] [Range(5f,20f)] float turningSpeed = 5f;
    [SerializeField][Range(0f, 20f)] float gravityValue = 18.81f;
    [SerializeField] private Animator _animator;
    [SerializeField] internal bool canMove = true;

    [Header("Animation")]
    [SerializeField] [Range(0.01f,1f)] private float animationBlend = 0.05f;

    // private variables
    
    private float tempSpeed = 5f;
    private Transform cameraTransform;
    private Movement _movement;

    internal Vector3 playerVelocity = Vector3.zero;
    
    //public variables
    public Vector3 PlayerVelocity {
        get {return new Vector3(input.x, _controller.velocity.y, input.y);}
        }

    //component references
    internal CharacterController _controller;

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

    /// <summary>
    /// This function is responsible for input vector2 movements
    /// </summary>
    /// <param name="ctx"></param>
    private void Move(InputAction.CallbackContext ctx)
    {
        if(ctx.performed){ input = ctx.ReadValue<Vector2>(); }
        else if(ctx.canceled){ input = Vector2.zero; }
    }
    private void OnEnable() {
        InputManager.inputActions.General.Move.performed += this.Move;
        InputManager.inputActions.General.Move.canceled += this.Move;
        InputManager.inputActions.General.Sprint.started += _ => {
            tempSpeed = playerSpeed; 
            playerSpeed *= sprintMultiplier;
            };
        InputManager.inputActions.General.Sprint.canceled += _ => {
            playerSpeed = tempSpeed;
            };
         _controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        // ----- Behavior -----
        Movement.Move(Time.deltaTime, input);
        canMove = Movement.CanMove;
       
        // ----- Animation -----
        Vector2 movementVelocity;
        movementVelocity.x = PlayerVelocity.x;
        movementVelocity.y = Mathf.Clamp(PlayerVelocity.magnitude,0f,1f);

        _animator.SetFloat("Blend_x",movementVelocity.x, animationBlend, Time.deltaTime);
        _animator.SetFloat("Blend_y",movementVelocity.y, animationBlend, Time.deltaTime);
    }
}
