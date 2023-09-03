using UnityEngine;
using Managers;

public class Attack : MonoBehaviour {
    
    private void Awake() {
         InputManager.inputActions.General.MouseClick.started += _ => GetComponent<Animator>().SetTrigger("Attack");   
    }
}