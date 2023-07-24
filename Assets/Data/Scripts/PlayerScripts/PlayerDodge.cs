using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Managers;
using Behaviours;

public class PlayerDodge : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] bool canDodge = true;
    [SerializeField][Range(1f,10f)] float dodgeSpeed = 2f;
    [SerializeField] private PlayerMovement _playerMove;
    [SerializeField] private bool isDodging;
    private PlayerState State = PlayerState.Gameplay;
    private Dodge _dodge;
    public Dodge DodgeBehaviour{
        get{
            if(_dodge == null)
            {
                return new Dodge(_animator,_playerMove.Movement,_playerMove._controller,dodgeSpeed, PlayerCameraHandler.Instance.camera);
            }
            return _dodge;
        }
    }

    private void Start() {
        _animator = GetComponent<Animator>();
        if(canDodge){
            InputManager.inputActions.General.Jump.started += _ =>DodgeBehaviour.StartDodge();
        }
    }

    public void Dodging() => DodgeBehaviour.Dodging();
    public void DodgeEnd() => DodgeBehaviour.DodgeEnd();
}
