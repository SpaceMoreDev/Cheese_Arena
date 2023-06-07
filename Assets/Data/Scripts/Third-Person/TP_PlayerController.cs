using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;


[RequireComponent(typeof(CharacterController))]
public class TP_PlayerController : MonoBehaviour
{
    private Vector3 playerVelocity;
    [SerializeField]
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float rotationSpeed = 5f;
    public static CharacterController controller;
    private Transform cameraTransform;
    private Vector3 velocity = Vector3.zero;

    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    
    #region Public Variables
    public CharacterObject playerCharacter;
    public static TP_PlayerController current;
    [SerializeField] private int playerID;
    [HideInInspector]public int ID {set{ playerID = value;} get{return playerID;}}
    #endregion

    void Awake()
    {
        current = this;
        DialogueManager.EndDialogueAction +=this.DialogueEnd;
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }
    

    void OnDestroy()
    {
        DialogueManager.EndDialogueAction -=this.DialogueEnd;
    }

    void DialogueEnd()
    {
        //when dialogue end
    }

    void Update()
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
        // Vector3 smoothedMove = Vector3.SmoothDamp(playerVelocity,move,ref velocity, playerSpeed *Time.deltaTime);
        controller.Move(move * playerSpeed *Time.deltaTime); //for input

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = Vector3.SmoothDamp(gameObject.transform.forward,move,ref velocity, playerSpeed *Time.deltaTime);
        }
            // Changes the height position of the player..
            if (PlayerInputHandler.Jump.triggered && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime); // for gravity
    }
        

    // public void Activate(CharacterObject sender,int id)
    // {
    //     if(this != PlayerManager.currentPlayer)
    //     {
    //         if(id == playerID)
    //         {
    //             if(playerCharacter.DialoguesFile != null)
    //             {
    //                 DialogueManager.StartDialogue(gameObject,playerCharacter,0);
    //                 NotificationManager.StartNotification($"Started dialogue with: [{playerCharacter.CharacterName}]");
    //             }
    //         }
    //     }
        
    // }
}
