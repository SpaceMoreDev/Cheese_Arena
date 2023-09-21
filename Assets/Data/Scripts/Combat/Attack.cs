using UnityEngine;
using Managers;
// willl execute this script on the frame of impact in the animation file.
public class Attack : MonoBehaviour {
    private void Awake() {
        if(PlayerMovement.current.canMove){
            Animator anim = GetComponent<Animator>();
            InputManager.inputActions.General.MouseClick.started += _ => {
                anim.SetTrigger("Attack");
            };  
        }
    }
}