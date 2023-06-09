using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Managers;

public enum PlayerState{
    UI,
    Gameplay
}

[RequireComponent(typeof(CharacterController))]
public class TP_PlayerController : MonoBehaviour
{    [SerializeField] public Health healthbar;
    
    private Vector3 playerVelocity;
    [SerializeField]
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float smoothTime = 2.0f;
    [SerializeField]
    private float blendSmooth = 2.0f;
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private Animator animator;
    public static PlayerState playerState;
    public static CharacterController controller;
    private Transform cameraTransform;
    Vector3 velocity = Vector3.zero;


    private float floatVelocity = 0;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    
    #region Public Variables
    public CharacterObject playerCharacter;
    public static TP_PlayerController current;
    [SerializeField] private int playerID;
    [HideInInspector]public int ID {set{ playerID = value;} get{return playerID;}}
    Vector2 input = Vector2.zero;
    bool alive = true;
    #endregion

    void Awake()
    {
        current = this;
        DialogueManager.EndDialogueAction +=this.DialogueEnd;
        InputManager.inputActions.General.MouseClick.started += Attack;
        InputManager.inputActions.General.Aim.started += Shield;
        InputManager.inputActions.General.Aim.canceled += Shield;
        Health.healthBarEmpty += ctx => Death(ctx);

    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        healthbar.character = gameObject;

        playerState = PlayerState.Gameplay;
    }
    

    void OnDestroy()
    {
        DialogueManager.EndDialogueAction -=this.DialogueEnd;
        InputManager.inputActions.General.MouseClick.started -= Attack;
        InputManager.inputActions.General.Aim.started -= Shield;
        InputManager.inputActions.General.Aim.canceled -= Shield;
        Health.healthBarEmpty -= ctx => Death(ctx);

    }

    void Death(GameObject ctx)
    {
        if(ctx == this.gameObject)
        {
            animator.Play("Death");
            alive = false;
        }
    }

    void DialogueEnd()
    {
        //when dialogue end
    }

    void Shield(InputAction.CallbackContext ctx)
    {
        if(alive && playerState == PlayerState.Gameplay)
        {
            if(ctx.started)
            {
                if(PlayerInputHandler.Movement.ReadValue<Vector2>() != Vector2.zero){
                    animator.SetBool("Shield",true);
                    animator.SetBool("Moving",true);
                }
                else{
                    animator.SetBool("Shield",true);
                    animator.SetBool("Moving",false);
                }
            } else if(ctx.canceled)
            {
                    animator.SetBool("Shield",false);
                    animator.SetBool("Moving",false);

            }
        }
    }

    void Attack(InputAction.CallbackContext ctx)
    {
        if(alive && playerState == PlayerState.Gameplay)
        {
            if(PlayerInputHandler.Movement.ReadValue<Vector2>() != Vector2.zero){
                animator.SetTrigger("Attack");
                animator.SetBool("Moving",true);
            }
            else{
                animator.SetTrigger("Attack");
                animator.SetBool("Moving",false);
            }
        }
    }

    void Update()
    {
        if(alive && playerState == PlayerState.Gameplay)
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 input = PlayerInputHandler.Movement.ReadValue<Vector2>();
            Vector3 move = new Vector3(input.x, 0, input.y);
            move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
            move.y = 0f;
            controller.Move(move * playerSpeed *Time.deltaTime); //for input
            

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = Vector3.SmoothDamp(gameObject.transform.forward,move,ref velocity, smoothTime *Time.deltaTime);
            }
                animator.SetFloat("Blend", controller.velocity.magnitude);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime); // for gravity
    }
}
