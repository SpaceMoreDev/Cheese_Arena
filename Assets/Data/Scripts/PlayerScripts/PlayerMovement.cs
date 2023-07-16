using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Managers;

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

    // private variables
    private Vector3 velocity = Vector3.zero; //temp velocity for lerping 
    private Vector3 playerVelocity = Vector3.zero;
    private bool groundedPlayer = false;
    private float tempSpeed = 5f;
    private Vector2  input = Vector2.zero;
    private Transform cameraTransform;

    //public variables
    public Vector3 PlayerVelocity {
        get {return new Vector3(input.x, _controller.velocity.y, input.y);}
        }

    //component references
    private CharacterController _controller;

    /// <summary>
    /// This function is responsible for input vector2 movements
    /// </summary>
    /// <param name="ctx"></param>
    private void Move(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            input = ctx.ReadValue<Vector2>();
        }
        else if(ctx.canceled)
        {
            input = Vector2.zero;
        }
    }

    private void Awake() {
        InputManager.inputActions.General.Move.performed += this.Move;
        InputManager.inputActions.General.Move.canceled += this.Move;
        InputManager.inputActions.General.Sprint.started += _ => {
            tempSpeed = playerSpeed; 
            playerSpeed *= sprintMultiplier;
            };
        InputManager.inputActions.General.Sprint.canceled += _ => {
            playerSpeed = tempSpeed;
            };
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = _controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector3 move;
        move = new Vector3(input.x, 0, input.y);

        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0f;
        _controller.Move(move * playerSpeed *Time.deltaTime); //for input
                
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = Vector3.SmoothDamp(gameObject.transform.forward,move,ref velocity,  turningSpeed *Time.deltaTime);
            // float targetAngle = cameraTransform.eulerAngles.y;
            // Quaternion rotation = Quaternion.Euler(0,targetAngle,0);
            // transform.rotation = Quaternion.Lerp(transform.rotation, rotation, turningSpeed * Time.deltaTime);
        }
        playerVelocity.y -= gravityValue * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime); // for gravity
    }
}
