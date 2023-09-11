using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Managers;
using Behaviours;

public class PlayerDodge : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] bool canDodge = true;
    [SerializeField] StaminaBar stamina;
    [SerializeField][Range(1f,10f)] float dodgeSpeed = 2f;
    [SerializeField] private PlayerMovement _playerMove;
    [SerializeField] private bool isDodging = false;
    [SerializeField] private float EnergyNeeded = 0.3f;
    private PLAYER_STATE State = PLAYER_STATE.Gameplay;
    private Dodge _dodge;
    public Dodge DodgeBehaviour{
        get{
            if(_dodge == null)
            {
                return new Dodge(_animator,_playerMove.Movement,_playerMove._controller,dodgeSpeed, PlayerCameraHandler.Instance.camera, _playerMove.input);
            }
            return _dodge;
        }
    }

    private void Start() {
        _animator = GetComponent<Animator>();
        if(canDodge){
            InputManager.inputActions.General.Jump.started += _ =>{
                if(!isDodging && stamina.Bar.Value > 0){
                    if(stamina.Bar.Value >= EnergyNeeded)
                    {
                        DodgeBehaviour.StartDodge();
                        _playerMove.SprintEnd();
                        stamina.DecreaseStamina(0.3f);
                        isDodging = true;
                    }
                }
            };
        }
    }

    public void Dodging() => DodgeBehaviour.Dodging();
    public void DodgeEnd() 
    {
        DodgeBehaviour.DodgeEnd();
        isDodging = false;
    }
}
