using UnityEngine;
using Managers;

public class Attack : MonoBehaviour {
    
    private void Awake() {
        if(PlayerMovement.current.canMove){
            InputManager.inputActions.General.MouseClick.started += _ => GetComponent<Animator>().SetTrigger("Attack");   
        }
    }
}