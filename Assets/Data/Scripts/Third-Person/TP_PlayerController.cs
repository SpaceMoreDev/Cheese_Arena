// using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Managers;

public enum PlayerState{
    UI,
    Gameplay
}

[RequireComponent(typeof(CharacterController))]
public class TP_PlayerController : MonoBehaviour
{   [SerializeField] 
    public Health healthbar;
    [SerializeField] 
    public ParticleSystem particles;
    private Vector3 playerVelocity;
    [SerializeField]
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float sprintSpeed = 2.0f;
     [SerializeField]
    private float dodgeSpeed = 2.0f;
    [SerializeField]
    private float smoothTime = 2.0f;
    [SerializeField]
    private float blendSmooth = 2.0f;
    [SerializeField]
    private float rotationSpeed = 5f;
    [SerializeField]
    private float attackRotationSpeed = 5f;
    [SerializeField]
    public Animator animator;
    public static PlayerState playerState;
    public static CharacterController controller;
    private Transform cameraTransform;
    Vector3 velocity = Vector3.zero;


    private float floatVelocity = 0;
    private float jumpHeight = 1.0f;
    private float gravityValue = -18.81f;

    
    #region Public Variables
    public CharacterObject playerCharacter;
    public static TP_PlayerController current;
    [SerializeField] private int playerID;
    [HideInInspector]public int ID {set{ playerID = value;} get{return playerID;}}
    Vector2 input = Vector2.zero;
    [HideInInspector] public bool alive = true;
    [HideInInspector] public bool dodging = false;
    [HideInInspector] public bool attacking = false;
    [HideInInspector] public bool blocked = false;
    [HideInInspector] public bool sprinting = false;
    #endregion

    void Awake()
    {
        current = this;
        animator = GetComponent<Animator>();
        DialogueManager.EndDialogueAction +=this.DialogueEnd;
        InputManager.inputActions.General.MouseClick.started += Attack;
        InputManager.inputActions.General.MouseClick.canceled += Attack;

        InputManager.inputActions.General.Aim.started += Shield;
        InputManager.inputActions.General.Aim.canceled += Shield;

        InputManager.inputActions.General.Move.performed += this.Move;
        InputManager.inputActions.General.Move.canceled += this.Move;

        InputManager.inputActions.General.Jump.started += _ => Dodge();
        InputManager.inputActions.General.Sprint.started += _ => Sprinting();
        InputManager.inputActions.General.Sprint.canceled += _ => EndSprinting();
        Health.healthBarEmpty += this.Death;

        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        healthbar.character = gameObject;

        playerState = PlayerState.Gameplay;
    }
    public void Move(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            animator.SetBool("moving",true);
            animator.SetLayerWeight(1,0);
            animator.SetLayerWeight(2,1);
            input = ctx.ReadValue<Vector2>();
        }
        else if(ctx.canceled)
        {
            animator.SetBool("moving",false);
            animator.SetLayerWeight(1,1);
            animator.SetLayerWeight(2,0);

            input = Vector2.zero;
        }
    }

    public void Dodge()
    {
        if(MainMenu.playing && !dodging)
        {
            if(alive && playerState == PlayerState.Gameplay)
            {
                blocked = false;
                attacking = false;
                sprinting = false;
                animator.SetBool("Shield",false);
                animator.SetTrigger("Dodge");
            }
        }
    }
    float tempSpeed;
    public void Sprinting()
    {
        sprinting = true;
        tempSpeed = playerSpeed;
        playerSpeed += sprintSpeed;
    }
    public void EndSprinting()
    {
        sprinting = false;
        playerSpeed = tempSpeed;
    }
    public void hitSword()
    {
        SwordHit.current.CheckHit();
    }
    void OnDestroy()
    {
        DialogueManager.EndDialogueAction -=this.DialogueEnd;
        InputManager.inputActions.General.MouseClick.started -= this.Attack;
        InputManager.inputActions.General.MouseClick.canceled -= this.Attack;

        InputManager.inputActions.General.Move.performed -= this.Move;
        InputManager.inputActions.General.Move.canceled -= this.Move;

        InputManager.inputActions.General.Aim.started -= this.Shield;
        InputManager.inputActions.General.Aim.canceled -= this.Shield;
        InputManager.inputActions.General.Jump.started -= _ => Dodge();
        InputManager.inputActions.General.Sprint.started -= _ => Sprinting();
        InputManager.inputActions.General.Sprint.canceled -= _ => EndSprinting();


        Health.healthBarEmpty -= this.Death;

    }

    void Death(GameObject ctx)
    {
        if(MainMenu.playing)
        {
            if(ctx == this.gameObject)
            {
                animator.SetBool("Shield",false);
                animator.SetBool("Attack", false);


                animator.Play("Death");
                GameOver.current.anim.Play("GameOver");
                alive = false;
                dodging = false;
                attacking = false;
                blocked = false;
                MainMenu.playing = false;
            }
        }
    }

    void DialogueEnd()
    {
        //when dialogue end
    }

    void Shield(InputAction.CallbackContext ctx)
    {
        if(MainMenu.playing && !dodging)
        {
            if(alive && playerState == PlayerState.Gameplay)
            {
                if(ctx.started)
                {
                    animator.SetBool("Shield",true);
                    blocked = true;
                } else if(ctx.canceled)
                {
                    animator.SetBool("Shield",false);
                    blocked = false;
                }
            }
        }
    }

    public void DodgeEnd()
    {
        playerVelocity.x = 0;
        playerVelocity.z = 0;
        dodging = false;
    }

    public void DodgeStart()
    {
        if(input != Vector2.zero)
        {
            Vector3 direction = new Vector3(input.x, 0, input.y);
            direction = direction.x * cameraTransform.right.normalized + direction.z * cameraTransform.forward.normalized;
            direction.y = 0f;
            transform.forward = direction;
        }
        Vector3 movementVelocity = transform.forward * dodgeSpeed;
        playerVelocity.x = movementVelocity.x;
        playerVelocity.z = movementVelocity.z;
        playerVelocity.y = 3;
        dodging = true;
        
        Debug.Log("Jumped!");
    }

    int count = 0;
    void Attack(InputAction.CallbackContext ctx)
    {
        if(MainMenu.playing && !dodging)
        {
            if(alive && playerState == PlayerState.Gameplay)
            {
                if(count<2)
                {

                    if(ctx.started)
                    {
                    // count++;
                        animator.SetBool("Attack", true);

                        attacking = true;
                    }
                }
                if(ctx.canceled)
                {

                    animator.SetBool("Attack", false);
                    
                }
            }
        }
    }

    public void EndAttack()
    {
        attacking = false;
        animator.SetBool("Attack", false);

        count = 0;
    }

    void Update()
    {

        if(MainMenu.playing)
        {
            if(!dodging)
            {
                if(alive && playerState == PlayerState.Gameplay)
                {
                    groundedPlayer = controller.isGrounded;
                    if (groundedPlayer && playerVelocity.y < 0)
                    {
                        playerVelocity.y = 0f;
                    }
                    Vector3 move = new Vector3(input.x, 0, input.y);
                    move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
                    move.y = 0f;
                    controller.Move(move * playerSpeed *Time.deltaTime); //for input
                    
                    if (move != Vector3.zero)
                    {
                        if(!blocked)
                        {
                            if(!sprinting)
                            {
                                gameObject.transform.forward = Vector3.SmoothDamp(gameObject.transform.forward,move,ref velocity, smoothTime *Time.deltaTime);
                            }
                            else{
                                gameObject.transform.forward = Vector3.SmoothDamp(gameObject.transform.forward,move,ref velocity, smoothTime * 0.5f *Time.deltaTime);

                            }
                        }
                        else
                        {
                            float targetAngle = cameraTransform.eulerAngles.y;
                            Quaternion rotation = Quaternion.Euler(0,targetAngle,0);
                            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
                        }
                    }
    
                    if(attacking)
                    {
                        float targetAngle = cameraTransform.eulerAngles.y;
                        Quaternion rotation = Quaternion.Euler(0,targetAngle,0);
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, attackRotationSpeed * Time.deltaTime);
                    }
                    if(!sprinting)
                    {
                        animator.SetFloat("Blend", Mathf.Clamp(controller.velocity.magnitude, 0, 0.5f), 0.1f, Time.deltaTime);
                    }
                    else
                    {
                        animator.SetFloat("Blend",controller.velocity.magnitude, 0.1f, Time.deltaTime);
                    }
                }
            }
            

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime); // for gravity
        }
    }
}
