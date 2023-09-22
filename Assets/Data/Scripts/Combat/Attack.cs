using UnityEngine;
using Managers;
using UnityEngine.InputSystem;
// willl execute this script on the frame of impact in the animation file.
public class Attack : MonoBehaviour {
    private Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
        InputManager.inputActions.General.MouseClick.started += AttackStarted;
    }

    private void OnDestroy() {
        InputManager.inputActions.General.MouseClick.started -= AttackStarted;
    }

    void AttackStarted(InputAction.CallbackContext ctx)
    {
        if(PlayerMovement.current.canMove){
            anim.SetTrigger("Attack");
        }
    }
}